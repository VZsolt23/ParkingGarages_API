using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingGarages_API.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Plate { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
