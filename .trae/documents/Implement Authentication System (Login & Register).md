I will implement the Backend Authentication following **SOLID principles** and including the specific roles you requested.

### Roles Definition
I will define the following roles in the system (using English keys for code stability):
1.  **Admin** (مدیر)
2.  **Supervisor** (سوپروایزر)
3.  **Nurse** (پرستار)
4.  **AssistantNurse** (کمک پرستار)
5.  **ElderlyCompanion** (سالمندیار)
6.  **MotherHelper** (مادریار)
7.  **Patient** (بیمار)
8.  **Elderly** (سالمند)
9.  **Family** (خانواده سالمند)

### SOLID Implementation Plan

#### 1. Application Layer (Abstractions)
*   **`IAuthService` Interface**: To satisfy the **Dependency Inversion Principle (DIP)**. The Controller will depend on this interface, not the implementation.
    *   `RegisterAsync(RegisterDto)`
    *   `LoginAsync(LoginDto)`
*   **DTOs**: `RegisterRequest`, `LoginRequest`, `AuthResponse` to separate API contracts from Domain entities (**Single Responsibility**).

#### 2. Infrastructure Layer (Implementation)
*   **`AuthService` Class**: Implements `IAuthService`. Handles the logic with `UserManager` and `JwtTokenGenerator`.
*   **`RoleSeeder`**: A service to automatically create the 9 roles in the database on startup.

#### 3. API Layer (Presentation)
*   **`AuthController`**: A thin controller that injects `IAuthService`. It only handles HTTP translation (200 OK, 400 Bad Request).

#### 4. Frontend Integration
*   Once the backend is ready, we will update the Register form to handle these roles.

I will start by creating the **DTOs** and the **IAuthService** interface. Ready?