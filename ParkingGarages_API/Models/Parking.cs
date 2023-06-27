using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingGarages_API.Models
{
    public class Parking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ParkingGarageId { get; set; }       
        public int CarId { get; set; }
        public DateTimeOffset StartOfParking { get; set; }
        public DateTimeOffset EndOfParking { get; set; }

        public ParkingGarage ParkingGarage { get; set; } = null!;
        public Car Car { get; set; } = null!;
    }
}
