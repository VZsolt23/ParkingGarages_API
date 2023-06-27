using ParkingGarages_API.Models.DTO;

namespace ParkingGarages_API.Repositories
{
    public interface IParkingRepository
    {
        Task<ParkingDTO> CreateParkingAsync(ParkingDTO parking);
        Task<IEnumerable<ParkingDTO>> GetOnGoingParkingsAsync();
    }
}
