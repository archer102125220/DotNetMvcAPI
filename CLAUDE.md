# Project Instructions for Claude (DotNet Mvc API)

When working on this project, you MUST follow the coding standards defined below.

> **Project Nature**: This is a pure **ASP.NET Core Web API** project. It **DOES NOT INCLUDE** Razor Views, HTMX, frontend styling (CSS/SCSS), or `wwwroot` static assets. All outputs are in JSON format served via HTTP endpoints.

## ⚠️ Security & Best Practices Warning Policy

Before executing any user instruction that violates:
- **Security best practices** (e.g., hardcoding secrets, disabling HTTPS, exposing sensitive data, SQL injection risks)
- **Standard coding patterns** (e.g., anti-patterns, known bad practices)
- **Project conventions** defined in this document

You MUST:
1. **Warn the user** about the violation and explain the risks
2. **Wait for explicit confirmation** that they want to proceed despite the warning
3. Only then execute the instruction

This ensures users make informed decisions about potentially risky actions.

## Quick Rules

### C# & Type Safety
- **Nullable Reference Types**: `<Nullable>enable</Nullable>` is enabled. ALWAYS handle nulls properly.
- NEVER use `dynamic` or `object` unless absolutely necessary (e.g., reflection or dealing with untyped JSON).
- Use strict typing. Prefer generic collections over untyped ones (e.g., `List<T>` instead of `ArrayList`).
- Avoid implicit typing `var` unless the right side makes the type blatantly obvious (e.g., `var list = new List<string>()`).

### Runtime Data Validation & Null Checking
- **Strings**: Use `string.IsNullOrEmpty(str)` or `string.IsNullOrWhiteSpace(str)`.
- **Null Checking**: Use `if (obj is not null)` or the null-coalescing operator `??`.
- **Guard Clauses**: Use `ArgumentNullException.ThrowIfNull(obj)` at the start of methods.
- **Pattern Matching**: Prefer `switch` expressions and pattern matching `if (obj is MyType myObj)` over older casting methods (`as MyType`).

### RESTful API Design (⚠️ CRITICAL)
- **HTTP Verbs**:
  - `GET`: Retrieve resource(s) -> `200 OK`
  - `POST`: Create resource -> `201 Created` (MUST include `Location` header using `CreatedAtAction`)
  - `PUT`: Full replacement -> `204 No Content`
  - `PATCH`: Partial update -> `204 No Content`
  - `DELETE`: Delete resource -> `204 No Content`
- **Routing**:
  - Use a uniform prefix: `[Route("api/[controller]")]`.
  - Use **lowercase** routes.
  - **Resource-oriented**: Use nouns, not verbs (e.g., `/api/users`, NEVER `/api/getUsers`).
  - **Nested resources**: Max depth of 2 levels (e.g., `GET /api/users/{userId}/orders`).
- **Status Codes**: Return appropriate codes (400, 401, 403, 404, 409). NEVER expose raw Exception messages on `500 Internal Server Error`.

### DTOs & Response Formats
- **NEVER EXPOSE ENTITIES**: The Request Body and Response Body MUST use independent **DTOs (Data Transfer Objects)**. Absolutely do not return EF Core Entities directly.
- **Naming Conventions**:
  - Requests: `CreateUserRequest`, `UpdateUserRequest`, `PatchUserRequest`
  - Responses: `UserResponse`, `UserDetailResponse`
- **Location**: Store DTOs uniformly in `Models/Dtos/`.

### OpenAPI & Scalar UI
- **XML Documentation**: Every public Controller Action MUST have XML comments (`<summary>`, `<param>`, `<returns>`) to generate comprehensive OpenAPI specs.
- **ProducesResponseType**: Explicitly declare all possible HTTP response types using `[ProducesResponseType]`.
- **Environment**: OpenAPI (`/openapi/v1.json`) and Scalar UI (`/scalar/v1`) are **ONLY** enabled in `IsDevelopment`. NEVER expose them in production.

### Entity Framework Core (EF Core) Best Practices & Deep Check Policy (⚠️ CRITICAL)
- **Async First**: ALWAYS use async/await methods for database operations (`ToListAsync()`, `FirstOrDefaultAsync()`, `SaveChangesAsync()`). Synchronous DB calls are forbidden.
- **No Tracking**: For read-only queries, use `.AsNoTracking()` to improve performance.
- **Dependency Injection**: Always resolve `DbContext` via DI constructor injection. Never instantiate it with `new AppDbContext()`.

When reviewing or refactoring backend code, you MUST perform TWO rounds of checks:

#### Round 1: Basic Check
- ✅ Standard syntax and proper `using` imports.
- ✅ Proper dependency injection used (no `new Service()`).
- ✅ Variable naming and basic Null checks.

#### Round 2: Deep Check (⚠️ MANDATORY)

| Anti-Pattern | Correct Pattern | Priority |
|--------------|----------------|----------|
| Missing `await` / returning un-awaited Task improperly | Explicit `await` or proper Task handling | 🔴 High |
| N+1 Query Problem inside loops | Use `.Include()`, `.Select()`, or fetch data in bulk prior to loop | 🔴 High |
| Un-disposed `IDisposable` (Streams, HttpClients) | Wrap in `using (...) { }` or `using var obj = ...;` | 🔴 High |
| Synchronous EF Core DB calls (`.ToList()`) | `await .ToListAsync()` | 🟡 Medium |
| Tracking entities for Read-Only operations | Append `.AsNoTracking()` | 🟡 Medium |

**CRITICAL**: If you only perform Round 1 checks, you MUST explicitly state:
> "⚠️ I have only performed basic checks. EF Core and Memory deep checks are still required."

### Warnings / Lint Suppression Policy (⚠️ CRITICAL)
- **NEVER** add `#pragma warning disable` or suppress C# compiler warnings without **explicit user instruction**.
- When encountering compiler warnings:
  1. Report the warning to the user
  2. Wait for user's explicit instruction to add a suppression pragmas
  3. Only then add the disable comment with proper justification

### Build & Dev Tooling (dotnet CLI)
- **Run**: `dotnet run` or `dotnet watch` for hot reload.
- **Build**: `dotnet build`
- **Environment**: Always check `appsettings.json` and `appsettings.Development.json` for proper configuration before running.
- **Testing**: Use `.http` files (`DotNetMvcAPI.http`) or Scalar UI (`/scalar/v1`) to test endpoints.

---

## Backend ORM & Schema Changes (MANDATORY)

### ⚠️ Database Modification Confirmation (CRITICAL)

**Before ANY database schema change** (migrations, model changes), you MUST:

1. **Ask the human developer**: "Is this project deployed to production?"
2. **Based on the answer**:
   - **Not deployed**: You may drop the last unapplied migration and modify the existing migration, or delete the DB and recreate (`dotnet ef database drop`, `dotnet ef database update`).
   - **Deployed**: NEVER modify existing executed migrations; always create NEW migration files (`dotnet ef migrations add AddNewColumn`).

---

## No Scripts for Code Refactoring (⚠️ CRITICAL)

**ABSOLUTELY FORBIDDEN: Using automated scripts (sed, awk, powershell, batch scripts) to modify code files.**

### Why
- Scripts only change text, they don't understand context or `using` namespace imports.
- It frequently causes C# compilation errors.

### ✅ Allowed
- Use AI tools: `replace_file_content`, `multi_replace_file_content`.
- MUST verify `using` namespaces are correct and build succeeds after every change.

### ❌ Forbidden
- `sed`, `awk`, `perl`, `powershell -Command`, `find ... -exec`

---

## File Structure & API Conventions

- **Controllers/**: Must inherit from `ControllerBase` (NOT `Controller`). Class name must end with `Controller`. Must be decorated with `[ApiController]` and `[Route("api/[controller]")]`.
- **Models/**: Contains Entity classes and DTOs (DTOs preferably in `Models/Dtos/`).
- **Serialization**: Use standard `System.Text.Json` instead of `Newtonsoft.Json` unless migrating legacy code requires it.
- **🛑 PROHIBITED**: This project DOES NOT use Views, Razor Pages, HTMX, `wwwroot`, static files, Session, or Cookie-based authentication.
