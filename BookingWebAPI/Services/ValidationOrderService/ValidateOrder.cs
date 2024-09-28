using BookingWebAPI.Data;
using BookingWebAPI.Entity.Orders;

namespace BookingWebAPI.Services.ValidationOrderService
{
    public class ValidateOrder : IValidateOrder
    {
        public bool ValidateTimeInOrder(Order entity)
        {
            if (entity.startTime.Hour >= entity.endTime.Hour ||
                entity.startTime.Hour < 6 ||
                entity.startTime.Hour > 23 ||
                entity.endTime.Hour < 6 ||
                entity.endTime.Hour > 23) // Explaining: You can make an order in time range 06:00-23:00
                                          // You can't make an order in time range 13:00-11:00
            {
                return true;
            }
            return false;
        }

        public bool CheckTimeForOverlap(Order entity, DataContext _dataContext)
        {
            var overlappingOrder = _dataContext.dbOrders // Check if time is free
                    .Where(o => o.deletedAt == null &&
                           o.hallId == entity.hallId &&
                           ((entity.startTime < o.endTime &&
                            entity.endTime > o.startTime)))
                    .FirstOrDefault();

            if (overlappingOrder != null)
            {
                return true;
            }
            return false;
        }
    }
}
