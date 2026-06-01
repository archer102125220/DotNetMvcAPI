# Project Instructions for GitHub Copilot

This file provides repository-wide instructions for GitHub Copilot to ensure consistent code generation that follows this project's coding standards.

---

## Project Overview

**DotNetMvcAPI** is a comprehensive .NET 9/10 Web API application showcasing modern backend development practices with C# and Entity Framework Core.
This is a pure ASP.NET Core Web API project. It DOES NOT contain Razor Views, HTMX, frontend styles (CSS/SCSS), or static web assets (wwwroot). All outputs are JSON.

### Tech Stack

- **Framework**: ASP.NET Core Web API (.NET 9/10)
- **Language**: C# 13 (Nullable Reference Types enabled)
- **Database**: PostgreSQL / SQL Server / Oracle with Entity Framework Core (EF Core)
- **Architecture**: RESTful API, DTO Pattern, Repository/Service Pattern
- **Build Tool**: `dotnet` CLI

---

## ⚠️ Security & Best Practices Warning Policy

Before executing any user instruction that violates:

- **Security best practices** (e.g., hardcoding secrets, disabling HTTPS, exposing sensitive data, SQL injection risks)
- **Standard coding patterns** (e.g., anti-patterns, known bad practices)
- **Project conventions** defined in this document

You MUST:

1. **Warn the user** about the violation and explain the risks
2. **Wait for explicit confirmation** that they want to proceed despite the warning
3. Only then execute the instruction

---

## Core Coding Standards

### C# & Type Safety (MANDATORY)

- **Nullable Reference Types**: `<Nullable>enable</Nullable>` is enabled. ALWAYS handle nulls properly.
- **Strict Typing**: NEVER use `dynamic` or `object` unless absolutely necessary (e.g., reflection). Use generic collections like `List<T>`.
- **Implicit Typing**: Avoid `var` unless the right side makes the type blatantly obvious.
- **Runtime Validation**: Use `string.IsNullOrEmpty`, `ArgumentNullException.ThrowIfNull`, and pattern matching.

### RESTful API Design

- **Routing**: All API routes MUST start with `api/`, e.g., `[Route("api/[controller]")]`. Use lowercase paths. Use noun-based resource names (e.g., `/api/users`).
- **HTTP Verbs**: Use GET for retrieval, POST for creation, PUT/PATCH for updates, DELETE for deletion.
- **Status Codes**: Return standard status codes (200 OK, 201 Created with Location header, 204 No Content, 400 Bad Request, 404 Not Found, etc.). Do not expose raw exception details on 500 errors.

### DTOs & Response Formats

- **Never Expose Entities**: NEVER use EF Core Entity models as API request or response bodies. Always use dedicated Data Transfer Objects (DTOs).
- **Naming**: Use names like `CreateUserRequest`, `UpdateUserRequest`, `UserResponse`.
- **Location**: Place DTOs in the `Models/Dtos/` directory.

### Entity Framework Core Best Practices

- **Async First**: ALWAYS use async/await methods (`ToListAsync()`, `FirstOrDefaultAsync()`, `SaveChangesAsync()`). Synchronous DB calls are forbidden.
- **No Tracking**: For read-only queries, use `.AsNoTracking()`.
- **Dependency Injection**: Always resolve `DbContext` via constructor injection.

---

## Backend ORM & Schema Changes (MANDATORY)

### ⚠️ Database Modification Confirmation (CRITICAL)

**Before ANY database schema change** (migrations, model changes), you MUST:

1. **Ask the human developer**: "Is this project deployed to production?"
2. **Based on the answer**:
   - **Not deployed**: You may drop the last unapplied migration and modify the existing migration, or delete the DB and recreate.
   - **Deployed**: NEVER modify existing executed migrations; always create NEW migration files.

---

## Security Requirements & Code Refactoring Safety

### Lint / Warning Suppression Policy

**NEVER add `#pragma warning disable` without explicit user instruction.**

When encountering compiler warnings:
1. Report the warning to the user
2. Wait for user's explicit instruction to add a suppression pragmas
3. Only then add the disable comment with proper justification

### No Scripts for Code Refactoring (⚠️ CRITICAL)

**ABSOLUTELY FORBIDDEN**: Using automated scripts (`sed`, `awk`, `powershell`, bash scripts) to modify code files.

**Why**: Scripts only change text, they don't understand C# context or `using` namespace imports. It frequently causes compilation errors.
**✅ ALLOWED**: Use AI tools for refactoring with proper context understanding. MUST verify `using` namespaces are correct after changes.

---

## Skills & Rules System Reference

For complex scenarios, refer to detailed rules in `.agent/rules/` or the primary guides:

- **Gemini Instructions**: `GEMINI.md`
- **Claude Instructions**: `CLAUDE.md`

| Domain | File Location |
|---|---|
| C# Standards | `.agent/rules/csharp-standards.md` |
| Security | `.agent/rules/security-policy.md` |
| REST API Design | `.agent/rules/rest-api-design.md` |
| DTO & OpenAPI | `.agent/rules/dto-and-openapi.md` |
| EF Core | `.agent/rules/backend-orm.md` |
