using System.ComponentModel.DataAnnotations;

namespace ParkingGarages_API.Models.DTO
{
    public class CarDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(9)]
        [RegularExpression("^[A-Z]{3}-[0-9]{3}$|^[A-Z]{2} [A-Z]{2}-[0-9]{3}$")]
        public string Plate { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
