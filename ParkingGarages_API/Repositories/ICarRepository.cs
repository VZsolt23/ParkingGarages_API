using Microsoft.AspNetCore.JsonPatch;
using ParkingGarages_API.Models.DTO;

namespace ParkingGarages_API.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<CarDTO>> GetAllCarssAsync();
        Task<CarDTO> GetCarAsync(int id);
        Task<CarDTO> CreateCarAsync(CarDTO carDTO);
        Task UpdateCarAsync(int id, CarDTO carDTO);
        Task UpdatePartialCarAsync(int id, JsonPatchDocument<CarDTO> patchDTO);
        Task DeleteCarAsync(int id);
    }
}
