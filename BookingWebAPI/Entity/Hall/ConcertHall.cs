using BookingWebAPI.Entity.Abstractions;

namespace BookingWebAPI.Entity.Hall
{
    public class ConcertHall : DbElement // Entity for concert hall
    {
        public String name { get; set; } = null!;
        public int amountOfPlaces { get; set; }
        public int basePricing { get; set; }
        public bool isProjectorAvailable { get; set; }
        public bool isWifiAvailable { get; set; }
        public bool isSoundAvailable { get; set; }

        public ConcertHall(string name, int amountOfPlaces, int basePricing, bool isProjectorAvailable, bool isWifiAvailable, bool isSoundAvailable, DateTime createdAt, DateTime updatedAt)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.amountOfPlaces = amountOfPlaces;
            this.basePricing = basePricing;
            this.isProjectorAvailable = isProjectorAvailable;
            this.isWifiAvailable = isWifiAvailable;
            this.isSoundAvailable = isSoundAvailable;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }
    }
}
