# Test2 Hotel API

## Endpoints

- `GET /api/rooms/{id}/guests` returns the room's complete reservation history,
  including guest and service details.
- `POST /api/guests` creates a guest and reservation in one transaction.

Example POST body:

```json
{
  "firstName": "John",
  "lastName": "Smith",
  "email": "john.smith@example.com",
  "phone": "123456789",
  "roomId": 1,
  "checkInDate": "2026-06-10",
  "checkOutDate": "2026-06-12",
  "status": "Confirmed"
}
```

Set `ConnectionStrings:DefaultConnection` in `Test2/appsettings.json`, then run:

```powershell
.\dotnet.cmd ef database update --project Test2\Test2.csproj
.\dotnet.cmd run --project Test2\Test2.csproj
```
