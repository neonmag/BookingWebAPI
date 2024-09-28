using BookingWebAPI.Entity.Hall;
using BookingWebAPI.Entity.Orders;
using Microsoft.EntityFrameworkCore;

namespace BookingWebAPI.Data
{
    public class DataContext : DbContext // Created a data context for using db
    {
        public DbSet<ConcertHall> dbConcertHall { get; set; } // Make a DbSets for creating tables
        public DbSet<Order> dbOrders { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>() // Added a relations
                .HasOne<ConcertHall>()
                .WithMany()
                .HasForeignKey(o => o.hallId);
        }
    }
}
