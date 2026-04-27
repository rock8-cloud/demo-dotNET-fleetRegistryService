# Fleet Registry Service

ASP.NET Core Web API for the Orbital Operations Platform demo.

The service manages spacecraft and captains. It uses Entity Framework Core with PostgreSQL, the `fleet_registry` schema, and JSON REST endpoints.

## Requirements

- .NET 8 SDK
- PostgreSQL

## Configuration

Use either a full connection string:

```bash
export CONNECTION_STRING="Host=localhost;Port=5432;Database=orbital_operations;Username=postgres;Password=postgres"
```

Or individual PostgreSQL variables:

```bash
export POSTGRES_HOST=localhost
export POSTGRES_PORT=5432
export POSTGRES_DB=orbital_operations
export POSTGRES_USER=postgres
export POSTGRES_PASSWORD=postgres
```

Defaults match the individual values above.

## Run

Start PostgreSQL if you do not already have a shared local instance:

```bash
docker compose up -d postgres
```

```bash
dotnet restore
dotnet run
```

The API listens on the default ASP.NET Core URL unless `ASPNETCORE_URLS` is set. For example:

```bash
ASPNETCORE_URLS=http://localhost:8081 dotnet run
```

## Run with Docker

Start the service and PostgreSQL:

```bash
docker compose up --build
```

The API will be available at `http://localhost:8081`.

Build the service image:

```bash
docker build -t fleet-registry-service .
```

Run it against PostgreSQL started by `docker compose`:

```bash
docker run --rm -p 8081:8080 \
  --network dotnet-fleetregistryservice_default \
  -e POSTGRES_HOST=postgres \
  -e POSTGRES_PORT=5432 \
  -e POSTGRES_DB=orbital_operations \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  fleet-registry-service
```

## Database

On startup, the service applies EF Core migrations. The initial migration creates:

- schema: `fleet_registry`
- migration history table: `fleet_registry.__EFMigrationsHistory`
- table: `fleet_registry.spacecraft`
- table: `fleet_registry.captain`

It also inserts demo spacecraft and captains directly in the initial migration.

## Project Structure

```text
Controllers/        HTTP endpoints
Domain/             domain/API objects
Services/           business rules and orchestration
Repositories/       persistence contracts used by services
Data/Repositories/  EF Core repository implementations
Data/Entities/      EF Core persistence entities
Data/Configurations EF Core entity mappings
Migrations/         EF Core migrations and demo seed data
Models/             request models
```

Controllers depend on services. Services depend on repository interfaces and domain objects only. EF Core, database entities, table mappings, and SQL details stay in the data layer.

## Endpoints

```http
GET /health
GET /fleet
GET /fleet/{id}
POST /fleet
GET /fleet/available
GET /captains
```

`GET /fleet/available` returns spacecraft where `status` is `ACTIVE`.

## Example Requests

Create a spacecraft:

```bash
curl -X POST http://localhost:8081/fleet \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Aurora",
    "type": "EXPLORER",
    "capacity": 10,
    "status": "ACTIVE"
  }'
```

List active spacecraft:

```bash
curl http://localhost:8081/fleet/available
```
