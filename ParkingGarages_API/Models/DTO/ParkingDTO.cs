namespace ParkingGarages_API.Models.DTO
{
    public class ParkingDTO
    {
        public int Id { get; set; }
        public int ParkingGarageId { get; set; }
        public int CarId { get; set; }
        public DateTimeOffset StartOfParking { get; set; }
        public DateTimeOffset EndOfParking { get; set; }
    }
}
