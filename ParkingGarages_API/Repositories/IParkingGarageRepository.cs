using Microsoft.AspNetCore.JsonPatch;
using ParkingGarages_API.Models.DTO;

namespace ParkingGarages_API.Repositories
{
    public interface IParkingGarageRepository
    {
        Task<IEnumerable<ParkingGarageDTO>> GetAllGaragesAsync();
        Task<ParkingGarageDTO> GetGarageAsync(int id);
        Task<ParkingGarageDTO> CreateGarageAsync(ParkingGarageDTO garageDTO);
        Task UpdateGarageAsync(int id, ParkingGarageDTO garageDTO);
        Task UpdatePartialGarageAsync(int id, JsonPatchDocument<ParkingGarageDTO> patchDTO);
        Task DeleteGarageAsync(int id);
    }
}
