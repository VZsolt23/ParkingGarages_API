using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingGarages_API.Models
{
    public class ParkingGarage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        public int NumberOfSpaces { get; set; }
    }
}
