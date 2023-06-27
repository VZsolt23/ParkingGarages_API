using AutoMapper;
using ParkingGarages_API.Models;
using ParkingGarages_API.Models.DTO;

namespace ParkingGarages_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ParkingGarage, ParkingGarageDTO>();
            CreateMap<ParkingGarageDTO, ParkingGarage>();

            CreateMap<Car, CarDTO>();
            CreateMap<CarDTO, Car>();

            CreateMap<Parking, ParkingDTO>();
            CreateMap<ParkingDTO, Parking>();
        }
    }
}
