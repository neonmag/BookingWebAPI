using BookingWebAPI.Entity.Hall;
using BookingWebAPI.Entity.Orders;

namespace BookingWebAPI.Repositories.IRepositories
{
    public interface IOrderRepository // Interface for repositories
                                      // In order to make a repository pattern
    {
        Task<Order?> GetById(Guid id);
        Task<IEnumerable<Order>> GetAll();
        Task<String> CreateAsync(Order entity);
        Task<String> UpdateAsync(Order entity);
        Task<String> DeleteAsync(Guid id);
        Task<IEnumerable<ConcertHall>> GetAvailableOrders(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity);
    }
}
