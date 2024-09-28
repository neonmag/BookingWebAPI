# BookingWebAPI
 Task for concert halls
## Overview
This is just a REST API, to work with it you must change connectionString in **appsettings.json** and make **dotnet ef database update** in PM Console

## Features
- **Concert halls Management**: Create, Read, Update, Delete
- **Management of reservation of concert halls**: Create, Read, Update, Delete
- **Search for free concert halls**: Task<IEnumerable<ConcertHall>> GetAvailableOrders(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity);
- **Dynamic price change**
- **Validation of input information at the stage of hall creation or reservation**

## Technologies used:
- **Backend**: ASP.NET Core
- **Database**: MySQL Workbench
- **ORM**: Entity Framework
- **Documentation**: Swagger

### Setup
1. **Clone the repository** https://github.com/neonmag/BookingWebAPI.git
2. **Setup your SQL database**
3. **Copy your connection string to database**
4. **Paste your connection string in appsettings.json**
5. **Run database migration**
   dotnet ef database update
6. **Run the application**
