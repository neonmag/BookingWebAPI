using BookingWebAPI.Entity.Hall;
using BookingWebAPI.Entity.Orders;

namespace BookingWebAPI.Services.CalculatingService
{
    public interface ICalculatingService
    {
        public float Calculate(ConcertHall hall, Order order);
    }
}
