# BookingWebAPI
 Task for concert halls
## Overview
This is just a REST API, to work with it you must change connectionString in **appsettings.json** and make **dotnet ef database update** in PM Console

## Features
- **Керування концертними залами**: Create, Read, Update, Delete
- **Керування резервуванням концертних зал**: Create, Read, Update, Delete
- **Пошук вільних концертних зал**: Task<IEnumerable<ConcertHall>> GetAvailableOrders(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity);
- **Динамічна зміна ціни**
- **Валідація вхідної інформації на етапі створення зали, або резервування**

## Technologies used:
- **Backend**: ASP.NET Core
- **Database**: MySQL Workbench
- **ORM**: Entity Framework
- **Documentation**: Swagger

### Setup
