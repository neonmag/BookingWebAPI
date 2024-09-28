using BookingWebAPI.Entity.Hall;
using BookingWebAPI.Entity.Orders;

namespace BookingWebAPI.Services.CalculatingService
{
    public class CalculatingService : ICalculatingService
    {
        // Service for calculating final price of order
        // To avoid cluttering the repository
        public float Calculate(ConcertHall hall, Order order)
        {
            if (hall == null)
            {
                throw new ArgumentNullException(nameof(hall), "Concert hall cannot be null.");
            }

            float priceForProperties = 0;
            priceForProperties = (hall!.isProjectorAvailable && order.isProjectorAvailable) ? priceForProperties + 500 : priceForProperties;
            priceForProperties = (hall!.isWifiAvailable && order.isWifiAvailable) ? priceForProperties + 300 : priceForProperties;
            priceForProperties = (hall!.isSoundAvailable && order.isSoundAvailable) ? priceForProperties + 700 : priceForProperties;

            float finalPrice = 0f;

            for (int i = order.startTime.Hour; i < order.endTime.Hour; i++)
            {
                if(i >= 12 && i <= 14)
                {
                    finalPrice += (hall!.basePricing + priceForProperties) + ((hall!.basePricing + priceForProperties) * 0.15f);
                }
                else if(i >= 9 && i <= 18)
                {
                    finalPrice += (hall!.basePricing + priceForProperties);
                }
                else if(i >= 6 && i <= 9)
                {
                    finalPrice += (hall!.basePricing + priceForProperties) - ((hall!.basePricing + priceForProperties) * 0.1f);
                }
                else if(i >= 18 && i <= 23)
                {
                    finalPrice += (hall!.basePricing + priceForProperties) - ((hall!.basePricing + priceForProperties) * 0.2f);
                }
            }

            return finalPrice;
        }
    }
}
