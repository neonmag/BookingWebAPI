using BookingWebAPI.Entity.Abstractions;

namespace BookingWebAPI.Entity.Orders
{
    public class Order : DbElement // Entity for order
    {
        public Guid hallId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public float finalPrice { get; set; }

        public bool isProjectorAvailable { get; set; }
        public bool isWifiAvailable { get; set; }
        public bool isSoundAvailable { get; set; }

        public Order(Guid hallId, DateTime startTime, DateTime endTime, float finalPrice, bool isProjectorAvailable, bool isWifiAvailable, bool isSoundAvailable, DateTime createdAt, DateTime updatedAt)
        {
            this.id = Guid.NewGuid();
            this.hallId = hallId;
            this.startTime = startTime;
            this.endTime = endTime;
            this.finalPrice = finalPrice;
            this.isProjectorAvailable = isProjectorAvailable;
            this.isWifiAvailable = isWifiAvailable;
            this.isSoundAvailable = isSoundAvailable;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }
    }
}
