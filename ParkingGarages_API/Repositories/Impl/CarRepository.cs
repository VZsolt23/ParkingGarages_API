using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingGarages_API.Data;
using ParkingGarages_API.Exceptions;
using ParkingGarages_API.Models;
using ParkingGarages_API.Models.DTO;
using System.Drawing;

namespace ParkingGarages_API.Repositories.Impl
{
    public class CarRepository : ICarRepository
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;

        public CarRepository(AppDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarDTO>> GetAllCarssAsync()
        {
            List<Car> cars = await _context.Cars.ToListAsync();
            List<CarDTO> carsDTO = _mapper.Map<List<CarDTO>>(cars);

            return carsDTO;
        }

        public async Task<CarDTO> GetCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return null;
            }

            var carDTO = _mapper.Map<CarDTO>(car);
            return carDTO;
        }

        public async Task<CarDTO> CreateCarAsync(CarDTO carDTO)
        {
            var existingCar = await _context.Cars.FirstOrDefaultAsync(c => c.Plate == carDTO.Plate);
            if (existingCar != null)
            {
                throw new CarConflict("A car with the same plate already exists.");
            }

            Car car = new Car()
            {
                Id = carDTO.Id,
                Plate = carDTO.Plate,
                Color = carDTO.Color,
                Type = carDTO.Type
            };

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return _mapper.Map<CarDTO>(car);
        }                      

        public async Task UpdateCarAsync(int id, CarDTO carDTO)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                throw new CarNotFound("The car is not found!");
            }

            var existingCar = await _context.Cars.FirstOrDefaultAsync(c => c.Plate == carDTO.Plate);
            if (existingCar != null)
            {
                throw new CarConflict("A car with the same plate already exists.");
            }

            car.Plate = carDTO.Plate;
            car.Color = carDTO.Color;
            car.Type = carDTO.Type;

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePartialCarAsync(int id, JsonPatchDocument<CarDTO> patchDTO)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                throw new CarNotFound("The car is not found!");
            }

            var carDTO = _mapper.Map<CarDTO>(car);
            patchDTO.ApplyTo(carDTO);

            if (!string.IsNullOrEmpty(carDTO.Plate))
            {
                var existingCar = await _context.Cars.FirstOrDefaultAsync(c => c.Plate == carDTO.Plate && c.Id != id);
                if (existingCar != null)
                {
                    throw new CarConflict("A car with the same plate already exists.");
                }
            }

            _mapper.Map(carDTO, car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                throw new CarNotFound("The car is not found!");
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
}
