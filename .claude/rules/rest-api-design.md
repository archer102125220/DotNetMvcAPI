# RESTful API Design

## HTTP Verbs
Use the appropriate HTTP verbs for operations:
- `GET`: Retrieve a resource or a collection of resources. Returns `200 OK`.
- `POST`: Create a new resource. Returns `201 Created`.
- `PUT`: Completely replace an existing resource. Returns `204 No Content`.
- `PATCH`: Partially update an existing resource. Returns `204 No Content`.
- `DELETE`: Remove a resource. Returns `204 No Content`.

## Route Naming
- **Prefix**: All API routes MUST start with `api/`, e.g., `[Route("api/[controller]")]`.
- **Nouns**: Routes should represent resources using nouns (e.g., `/api/users`), NOT verbs (e.g., `/api/getUsers`).
- **Nesting**: Use nested routes for related resources, but limit depth to two levels (e.g., `GET /api/users/{userId}/orders`).
- **Casing**: Use lowercase paths.

## HTTP Status Codes
- `200 OK`: Successful query with data.
- `201 Created`: Successful creation. Must include the `Location` header (using `CreatedAtAction`).
- `204 No Content`: Successful operation with no return data (PUT, PATCH, DELETE).
- `400 Bad Request`: Client error or validation failure.
- `401 Unauthorized`: Authentication required.
- `403 Forbidden`: Authenticated but insufficient permissions.
- `404 Not Found`: Resource does not exist.
- `409 Conflict`: Resource conflict (e.g., duplicate).
- `500 Internal Server Error`: Server-side error. DO NOT expose raw exception messages to the client.
