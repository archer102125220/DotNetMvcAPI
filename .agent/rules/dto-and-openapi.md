# DTOs and OpenAPI

## Data Transfer Objects (DTOs)
- **Separation of Concerns**: The Request Body and Response Body of an API MUST use dedicated DTO classes.
- **NEVER Expose Entities**: Absolutely do not use EF Core Entity models as API return types or request types.
- **Naming Conventions**:
  - Requests: `CreateUserRequest`, `UpdateUserRequest`, `PatchUserRequest`
  - Responses: `UserResponse`, `UserDetailResponse`
- **Location**: Place all DTOs in the `Models/Dtos/` directory.

## OpenAPI (Swagger/Scalar) Documentation
- **XML Comments**: This project has `<GenerateDocumentationFile>true</GenerateDocumentationFile>` enabled. All public Controller Actions MUST have XML documentation comments (`<summary>`, `<param>`, `<returns>`).
- **Response Types**: Use the `[ProducesResponseType]` attribute on actions to explicitly declare all possible HTTP status codes and their associated return types. This ensures the OpenAPI spec is complete.
- **Environment**: The OpenAPI document (`/openapi/v1.json`) and Scalar UI (`/scalar/v1`) must ONLY be enabled in the Development environment. NEVER expose these endpoints in Production.
