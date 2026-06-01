# Project Instructions for AI Agents (DotNet Web API)

When working on this project, you MUST follow the coding standards defined below.
This project is a pure ASP.NET Core Web API project. It DOES NOT contain Razor Views, HTMX, frontend styles (CSS/SCSS), or static web assets (wwwroot). All outputs are JSON.

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
- **Routing**: All API routes MUST start with `api/`, e.g., `[Route("api/[controller]")]`. Use lowercase paths. Use noun-based resource names (e.g., `/api/users`, NOT `/api/getUsers`).
- **HTTP Verbs**: Use appropriate verbs (GET for retrieval, POST for creation, PUT/PATCH for updates, DELETE for deletion).
- **Status Codes**: Return standard status codes (200 OK, 201 Created with Location header, 204 No Content, 400 Bad Request, 404 Not Found, etc.). Do not expose raw exception details on 500 errors.

### DTOs & Response Formats (⚠️ CRITICAL)
- **Never Expose Entities**: NEVER use EF Core Entity models as API request or response bodies. Always use dedicated Data Transfer Objects (DTOs).
- **Naming**: Use names like `CreateUserRequest`, `UpdateUserRequest`, `UserResponse`.
- **Location**: Place DTOs in the `Models/Dtos/` directory.

### OpenAPI & Documentation
- Ensure all public Controller Actions are documented using XML Comments (`<summary>`, `<param>`, `<returns>`) and `[ProducesResponseType]` to generate complete OpenAPI specs.
- The OpenAPI JSON and Scalar UI endpoints are ONLY available in the Development environment.

### C# & ASP.NET Core Stable APIs (⚠️ CRITICAL)
- **Prioritize standard ASP.NET Core Web API patterns** and avoid experimental NuGet packages or unsupported .NET features.
- Default to standard `ControllerBase`, Dependency Injection via constructor, and standard API routing.
- Use `System.Text.Json` instead of Newtonsoft unless specifically required by legacy code.

### EF Core & Memory Deep Check Policy (⚠️ CRITICAL)

When reviewing or refactoring backend code (C# Controllers, Services, Data Access), you MUST perform TWO rounds of checks:

#### Round 1: Basic Check
- ✅ Standard syntax and proper `using` imports.
- ✅ Proper dependency injection used (no `new Service()`).
- ✅ Variable naming and basic Null checks.

#### Round 2: Deep Check (⚠️ MANDATORY)
You MUST check for these common mistakes:

| Anti-Pattern | Correct Pattern | Priority |
|--------------|----------------|----------|
| Missing `await` / returning un-awaited Task improperly | Explicit `await` or proper Task handling | 🔴 High |
| N+1 Query Problem inside loops | Use `.Include()`, `.Select()`, or fetch data in bulk prior to loop | 🔴 High |
| Un-disposed `IDisposable` (Streams, HttpClients) | Wrap in `using (...) { }` or `using var obj = ...;` | 🔴 High |
| Synchronous EF Core DB calls (`.ToList()`) | `await .ToListAsync()` | 🟡 Medium |
| Tracking entities for Read-Only operations | Append `.AsNoTracking()` | 🟡 Medium |

**CRITICAL**: If you only perform Round 1 checks, you MUST explicitly state:
> "⚠️ I have only performed basic checks. EF Core and Memory deep checks are still required."

**When to use the Deep Check Rule**:
- When asked to "check" or "review" C# code.
- When refactoring backend services or controllers.
- When optimizing database queries or memory usage.

### Warnings / Lint Suppression Policy (⚠️ CRITICAL)
- **NEVER** add `#pragma warning disable` or suppress C# compiler warnings without **explicit user instruction**.
- When encountering compiler warnings:
  1. Report the warning to the user
  2. Wait for user's explicit instruction to add a suppression pragmas
  3. Only then add the disable comment with proper justification

### Build & Dev Tooling (dotnet CLI)
- **Run**: `dotnet run` or `dotnet watch` for hot reload.
- **Build**: `dotnet build`
- **EF Core CLI**: Use `dotnet ef` tools for migrations (e.g. `dotnet ef migrations add`, `dotnet ef database update`).
- **Environment**: Always check `appsettings.json` and `appsettings.Development.json` for proper configuration before running.

---

## Backend ORM & Schema Changes (MANDATORY)

### ⚠️ Database Modification Confirmation (CRITICAL)

**Before ANY database schema change** (migrations, model changes), you MUST:

1. **Ask the human developer**: "Is this project deployed to production?"
2. **Based on the answer**:
   - **Not deployed**: You may drop the last unapplied migration and modify the existing migration, or delete the DB and recreate (`dotnet ef database drop`, `dotnet ef database update`).
   - **Deployed**: NEVER modify existing executed migrations; always create NEW migration files (`dotnet ef migrations add AddNewColumn`).

### Migrations Workflow
- Use `dotnet ef migrations add <MigrationName>` to create a migration.
- Use `dotnet ef database update` to apply migrations.
- Always review the generated migration C# file before applying it to ensure EF Core scaffolded it correctly.

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

- **Controllers/**: Must inherit from `ControllerBase` and be decorated with `[ApiController]`. End class name with `Controller`.
- **Models/**: Entity classes.
- **Models/Dtos/**: Data Transfer Objects (DTOs) for API requests and responses.

For more detailed rules, you MUST review the specific files located in the `.agent/rules/` directory:
- [csharp-standards.md](.agent/rules/csharp-standards.md): C# Language and Type Safety rules
- [rest-api-design.md](.agent/rules/rest-api-design.md): RESTful API route and verb conventions
- [dto-and-openapi.md](.agent/rules/dto-and-openapi.md): DTO models and OpenAPI generation
- [runtime-data-validation.md](.agent/rules/runtime-data-validation.md): Runtime Null & Data Validation
- [security-policy.md](.agent/rules/security-policy.md): Security Policies
- [i18n.md](.agent/rules/i18n.md): Localization / i18n
- [build-tools.md](.agent/rules/build-tools.md): .NET Build & Dev Tooling
- [file-organization.md](.agent/rules/file-organization.md): API Architecture & Structure
- [lint-policy.md](.agent/rules/lint-policy.md): Warnings & Suppression rules
- [backend-orm.md](.agent/rules/backend-orm.md): EF Core & Migrations
- [no-scripts.md](.agent/rules/no-scripts.md): No Bash/Sed Script Refactoring
- [project-instructions.md](.agent/rules/project-instructions.md): Overall instructions
