using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ParkingGarages_API.Data;
using ParkingGarages_API.Models;
using ParkingGarages_API.Models.DTO;

namespace ParkingGarages_API.Repositories.Impl
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public ParkingRepository(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParkingDTO> CreateParkingAsync(ParkingDTO parkingDTO)
        {
            bool isCarAlreadyParked = await _context.Parkings.AnyAsync(p =>
                p.CarId == parkingDTO.CarId &&
                p.StartOfParking <= DateTimeOffset.UtcNow &&
                p.EndOfParking >= DateTimeOffset.UtcNow);

            if (isCarAlreadyParked)
            {
                throw new InvalidOperationException("The car is already parked in another parking space.");
            }

            if (parkingDTO.EndOfParking <= DateTime.UtcNow)
            {
                throw new InvalidOperationException("The expiry date cannot be earlier than or equal to the start date.");
            }

            Parking parking = new Parking()
            {
                Id = parkingDTO.Id,
                ParkingGarageId = parkingDTO.ParkingGarageId,
                CarId = parkingDTO.CarId,
                StartOfParking = DateTime.UtcNow,
                EndOfParking = parkingDTO.EndOfParking
            };

            await _context.Parkings.AddAsync(parking);
            await _context.SaveChangesAsync();

            return _mapper.Map<ParkingDTO>(parking);
        }

        public async Task<IEnumerable<ParkingDTO>> GetOnGoingParkingsAsync()
        {
            DateTimeOffset currentTime = DateTimeOffset.UtcNow;

            List<Parking> parkings = await _context.Parkings
                .Where(p => p.StartOfParking <= currentTime && p.EndOfParking >= currentTime)
                .ToListAsync();

            List<ParkingDTO> parkingsDTO = _mapper.Map<List<ParkingDTO>>(parkings);

            return parkingsDTO;
        }
    }
}
