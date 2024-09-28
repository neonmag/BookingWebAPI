using BookingWebAPI.Data;
using BookingWebAPI.Entity.Hall;
using BookingWebAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BookingWebAPI.Repositories.Hall
{
    public class HallRepository : IHallRepository // Repository class for manipulating data in db
    {
        private readonly DataContext _dataContext;

        public HallRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<String> GetByName(String name) // Get hall by name
        {
            var result = await _dataContext.dbConcertHall
                .Where(c => c.deletedAt == null && c.name == name)
                .FirstOrDefaultAsync();

            if(result != null)
            {
                return $"Hall with name: {name} already exists";
            }

            return $"{name} is free";
        }

        public async Task<IEnumerable<ConcertHall>> GetAll() // Get all halls
        {
            return await _dataContext.dbConcertHall
                .Where(c => c.deletedAt == null)
                .ToListAsync();
        }

        public async Task<ConcertHall?> GetById(Guid id) // Get hall by id
        {
            return await _dataContext.dbConcertHall
                .Where(c => c.deletedAt == null && c.id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<String> CreateAsync(ConcertHall entity) // Create hall
        {
            var isTableEmpty = !await _dataContext.dbConcertHall.AnyAsync(); // If hall table is empty - we should make a 3 halls
                                                                             // Hall A, B, C (as stated in the assignment)
            if (isTableEmpty)
            {
                ConcertHall firstPlace = new ConcertHall("Hall A", 50, 2000, true, true, true, DateTime.Now, DateTime.Now);
                ConcertHall secondPlace = new ConcertHall("Hall B", 100, 3500, true, true, true, DateTime.Now, DateTime.Now);
                ConcertHall thirdPlace = new ConcertHall("Hall C", 30, 1500, true, true, true, DateTime.Now, DateTime.Now);

                await _dataContext.dbConcertHall.AddRangeAsync(firstPlace, secondPlace, thirdPlace);
                await _dataContext.SaveChangesAsync();
            }

            if (entity != null)
            {
                entity.updatedAt = DateTime.Now;
                entity.createdAt = DateTime.Now;
                await _dataContext.dbConcertHall.AddAsync(entity);
                await _dataContext.SaveChangesAsync();
                return $"Concert hall created with id: {entity.id}";
            }

            return "Entity is null";
        }

        public async Task<String> DeleteAsync(Guid id) // Delete method (mark deletedAt)
        {
            var element = await _dataContext.dbConcertHall
                .Where(c => c.deletedAt == null && c.id == id)
                .FirstOrDefaultAsync();

            if (element != null)
            {
                element.updatedAt = DateTime.Now;
                element.deletedAt = DateTime.Now;
                await _dataContext.SaveChangesAsync();

                return $"Hall with id: {id} deleted";
            }
            return "Fail";
        }

        public async Task<String> UpdateAsync(ConcertHall entity) // Update method
        {
            var existingEntity = await _dataContext.dbConcertHall
                  .Where(c => c.deletedAt == null && c.id == entity.id)
                  .FirstOrDefaultAsync();

            if (existingEntity != null)
            {
                existingEntity.name = entity.name;
                existingEntity.amountOfPlaces = entity.amountOfPlaces;
                existingEntity.basePricing = entity.basePricing;
                existingEntity.isWifiAvailable = entity.isWifiAvailable;
                existingEntity.isProjectorAvailable = entity.isProjectorAvailable;
                existingEntity.isSoundAvailable = entity.isSoundAvailable;
                existingEntity.updatedAt = DateTime.Now;

                await _dataContext.SaveChangesAsync();

                return $"Hall with id: {entity.id} updated";
            }

            return $"Some error";
        }
    }
}
