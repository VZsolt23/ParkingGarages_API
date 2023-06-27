using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ParkingGarages_API.Data;
using ParkingGarages_API.Exceptions;
using ParkingGarages_API.Models;
using ParkingGarages_API.Models.DTO;
using System.Net;

namespace ParkingGarages_API.Repositories.Impl
{
    public class ParkingGarageRepository : IParkingGarageRepository
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public ParkingGarageRepository(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParkingGarageDTO>> GetAllGaragesAsync()
        {
            List<ParkingGarage> garages = await _context.ParkingGarages.ToListAsync();
            List<ParkingGarageDTO> garagesDTO = _mapper.Map<List<ParkingGarageDTO>>(garages);

            return garagesDTO;
        }

        public async Task<ParkingGarageDTO> GetGarageAsync(int id)
        {
            var garage = await _context.ParkingGarages.FindAsync(id);

            if (garage == null)
            {
                return null;
            }

            var garageDTO = _mapper.Map<ParkingGarageDTO>(garage);
            return garageDTO;
        }

        public async Task<ParkingGarageDTO> CreateGarageAsync(ParkingGarageDTO garageDTO)
        {
            ParkingGarage garage = new ParkingGarage()
            {
                Id = garageDTO.Id,
                Address = garageDTO.Address,
                NumberOfSpaces = garageDTO.NumberOfSpaces
            };
            
            await _context.ParkingGarages.AddAsync(garage);
            await _context.SaveChangesAsync();

            return _mapper.Map<ParkingGarageDTO>(garage);
        }

        public async Task UpdateGarageAsync(int id, ParkingGarageDTO garageDTO)
        {
            var garage = await _context.ParkingGarages.FindAsync(id);

            if (garage == null)
            {
                throw new GarageNotFound("The parking garage is not found!");
            }

            garage.Address = garageDTO.Address;
            garage.NumberOfSpaces = garageDTO.NumberOfSpaces;

            await _context.SaveChangesAsync();

        }

        public async Task UpdatePartialGarageAsync(int id, JsonPatchDocument<ParkingGarageDTO> patchDTO)
        {
            var garage = await _context.ParkingGarages.FindAsync(id);

            if (garage == null)
            {
                throw new GarageNotFound("The parking garage is not found!");
            }

            var garageDTO = _mapper.Map<ParkingGarageDTO>(garage);
            patchDTO.ApplyTo(garageDTO);

            _mapper.Map(garageDTO, garage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGarageAsync(int id)
        {
            var garage = await _context.ParkingGarages.FindAsync(id);

            if (garage == null)
            {
                throw new GarageNotFound("The parking garage is not found!");
            }

            _context.ParkingGarages.Remove(garage);
            await _context.SaveChangesAsync();
        }    
    }
}
