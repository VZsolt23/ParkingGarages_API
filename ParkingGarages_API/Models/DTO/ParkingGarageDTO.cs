namespace ParkingGarages_API.Models.DTO
{
    public class ParkingGarageDTO
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public int NumberOfSpaces { get; set; }
    }
}
