namespace BookingWebAPI.Entity.Abstractions
{
    public abstract class DbElement // Abstract class for creating some required fields in tables
    {
        public Guid id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
    }
}
