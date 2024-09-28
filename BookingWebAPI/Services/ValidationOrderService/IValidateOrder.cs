using BookingWebAPI.Data;
using BookingWebAPI.Entity.Orders;

namespace BookingWebAPI.Services.ValidationOrderService
{
    public interface IValidateOrder
    {
        bool ValidateTimeInOrder(Order entity);
        bool CheckTimeForOverlap(Order entity, DataContext _dataContext);
    }
}
