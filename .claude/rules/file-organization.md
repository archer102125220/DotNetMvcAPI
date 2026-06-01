# API Architecture & Structure

## 1. Controllers
- Keep Controllers thin. They should handle HTTP request routing, binding, and returning HTTP status codes with JSON responses.
- Business logic belongs in Services or Domain classes.
- All controllers MUST inherit from `ControllerBase` and be decorated with `[ApiController]` and `[Route("api/[controller]")]`.

## 2. Models
- **Entities**: Represents database tables. Place in `Models/Entities`.
- **DTOs**: Data Transfer Objects (Data Transfer Objects) used for API requests and responses. Place in `Models/Dtos/`. NEVER return Entities directly from a controller.

## 3. Services
- Business logic should be encapsulated in Service classes.
- Place interfaces and their implementations in the `Services/` directory.

## 4. Data
- Entity Framework Core `DbContext` and configurations (e.g., `IEntityTypeConfiguration`) belong in the `Data/` directory.
