namespace ParkingGarages_API.Exceptions
{
    public class CarConflict : Exception
    {
        public CarConflict(string message) : base(message)
        {
        }
    }
}
