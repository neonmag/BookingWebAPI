using BookingWebAPI.Entity.Hall;

namespace BookingWebAPI.Repositories.IRepositories
{
    public interface IHallRepository // Interface for repositories
                                    // In order to make a repository pattern
    {
        Task<String> GetByName(String name);
        Task<ConcertHall?> GetById(Guid id);
        Task<IEnumerable<ConcertHall>> GetAll();
        Task<String> CreateAsync(ConcertHall entity);
        Task<String> UpdateAsync(ConcertHall entity);
        Task<String> DeleteAsync(Guid id);
    }
}
