namespace ParkingGarages_API.Exceptions
{
    public class CarNotFound: Exception
    {
        public CarNotFound(string message) : base(message)
        {
        }
    }
}
