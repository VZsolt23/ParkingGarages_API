using System.Net;

namespace ParkingGarages_API.Exceptions
{
    public class GarageNotFound : Exception
    {
        public GarageNotFound(string message) : base(message)
        {
        }
    }
}
