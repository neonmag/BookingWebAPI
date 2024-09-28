using BookingWebAPI.Data;
using BookingWebAPI.Entity.Hall;
using BookingWebAPI.Entity.Orders;
using BookingWebAPI.Repositories.IRepositories;
using BookingWebAPI.Services.CalculatingService;
using BookingWebAPI.Services.ValidationOrderService;
using Microsoft.EntityFrameworkCore;

namespace BookingWebAPI.Repositories.Orders
{
    public class OrderRepository : IOrderRepository // Repository class to manipulate info in db
    {
        private readonly DataContext _dataContext;
        private readonly ICalculatingService _calculatingService;
        private readonly IValidateOrder _validateOrder;

        public OrderRepository(DataContext dataContext, ICalculatingService calculatingService, IValidateOrder validateOrder)
        {
            this._dataContext = dataContext;
            this._calculatingService = calculatingService;
            this._validateOrder = validateOrder;
        }

        public async Task<String> CreateAsync(Order entity) // Make an order with specific rules
        {
            if (_validateOrder.ValidateTimeInOrder(entity)) // Explaining: You can make an order in time range 06:00-23:00
                                          // You can't make an order in time range 13:00-11:00
            {
                return "Invalid time";
            }

            if (_validateOrder.CheckTimeForOverlap(entity, _dataContext))
            {
                return "Time isn't free";
            }


            if (entity != null)
            {
                entity.updatedAt = DateTime.Now;
                entity.createdAt = DateTime.Now;

                var hall = await _dataContext.dbConcertHall.FindAsync(entity.hallId);

                entity.finalPrice = _calculatingService.Calculate(hall!, entity); // Calculate final price in order

                await _dataContext.dbOrders.AddAsync(entity);
                await _dataContext.SaveChangesAsync();
                return $"Order created with id: {entity.id}";
            }

            return "Entity is null";
        }

        public async Task<String> DeleteAsync(Guid id) // Mark order as deleted
        {
            var existingElement = await _dataContext.dbOrders
                .Where(o => o.deletedAt == null && o.id == id)
                .FirstOrDefaultAsync();

            if (existingElement != null)
            {
                existingElement.deletedAt = DateTime.Now;
                existingElement.updatedAt = DateTime.Now;

                await _dataContext.SaveChangesAsync();
                return $"Order with id: {id} deleted";
            }
            return "Fail";
        }

        public async Task<IEnumerable<Order>> GetAll() // Get all orders
        {
            return await _dataContext.dbOrders
                .Where(o => o.deletedAt == null)
                .ToListAsync();
        }

        public async Task<Order?> GetById(Guid id) // Get one order by id
        {
            return await _dataContext.dbOrders
                .Where(o => o.deletedAt == null && o.id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<String> UpdateAsync(Order entity) // Update an order with a specific rules, like as creating statement
        {
            if (_validateOrder.ValidateTimeInOrder(entity)) // Explaining: You can make an order in time range 06:00-23:00
                                                            // You can't make an order in time range 13:00-11:00
            {
                return "Invalid time";
            }

            if (_validateOrder.CheckTimeForOverlap(entity, _dataContext))
            {
                return "Time isn't free";
            }

            var existingElement = await _dataContext.dbOrders
                .Where(c => c.deletedAt == null && c.id == entity.id)
                .FirstOrDefaultAsync();

            if (existingElement != null)
            {
                var hall = await _dataContext.dbConcertHall.FindAsync(entity.hallId);

                existingElement.finalPrice = _calculatingService.Calculate(hall!, entity);
                existingElement.hallId = entity.hallId;
                existingElement.startTime = entity.startTime;
                existingElement.endTime = entity.endTime;
                existingElement.updatedAt = DateTime.Now;

                await _dataContext.SaveChangesAsync();

                return $"Order with id: {entity.id} updated";
            }

            return $"Some error";
        }

        public async Task<IEnumerable<ConcertHall>> GetAvailableOrders(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity)
        {
            // Get all available orders in selected date, time and hall capacity
            var availableHalls = await _dataContext.dbConcertHall
                .Where(hall => hall.amountOfPlaces >= capacity)
                .ToListAsync();

            var occupiedHalls = await _dataContext.dbOrders
                .Where(order => order.deletedAt == null &&
                                order.startTime.Date == date.Date && 
                                ((startTime < order.endTime.TimeOfDay && endTime > order.startTime.TimeOfDay))) 
                .Select(order => order.hallId)
                .ToListAsync();

            return availableHalls
                .Where(hall => !occupiedHalls.Contains(hall.id)) 
                .ToList();
        }
    }
}
