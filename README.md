# 🍽️ Restaurants API

## 📌 Overview
The **Restaurants API** is a secure and scalable backend built with **ASP.NET Core**. It provides restaurant and dish management functionalities with a structured authentication and authorization system. The API follows **Clean Architecture**, **CQRS**, and **Specification Pattern** to ensure maintainability and performance.

---

## 🚀 Features
### 🏗 **Project Structure & Design**
- **Clean Architecture**: Ensures modularity and separation of concerns.
- **CQRS with MediatR**: Enhances query and command handling.
- **Entity Framework Core with Repository & Specification Pattern**: Manages database interactions efficiently.

### 🔍 **API Modules**
#### **1️⃣ Accounts (User Management & Authentication)**
- **User Registration & Login**
  - `POST /api/Accounts/register` – Register a new user.
  - `POST /api/Accounts/login` – Authenticate and obtain JWT token.
- **Token Management**
  - `POST /api/Accounts/refresh` – Refresh JWT token.
- **Email & Security**
  - `GET /api/Accounts/confirmEmail` – Confirm user email.
  - `POST /api/Accounts/resendConfirmationEmail` – Resend email confirmation.
  - `POST /api/Accounts/forgotPassword` – Request password reset.
  - `POST /api/Accounts/resetPassword` – Reset password.
- **Two-Factor Authentication (2FA)**
  - `POST /api/Accounts/manage/2fa` – Enable or disable 2FA.
- **User Roles & Permissions**
  - `POST /api/Accounts/userRole` – Assign a role to a user.
  - `DELETE /api/Accounts/userRole` – Remove a user role.

#### **2️⃣ Restaurants (CRUD Operations for Restaurants)**
- `GET /api/Restaurants` – Retrieve all restaurants.
- `POST /api/Restaurants` – Add a new restaurant.
- `GET /api/Restaurants/{id}` – Retrieve details of a specific restaurant.
- `PUT /api/Restaurants/{id}` – Update restaurant information.
- `DELETE /api/Restaurants/{id}` – Delete a restaurant.

#### **3️⃣ Dishes (Managing Dishes for Restaurants)**
- `POST /api/restaurant/{restaurantId}/dishes` – Add a dish to a restaurant.
- `GET /api/restaurant/{restaurantId}/dishes` – Retrieve all dishes for a restaurant.
- `PUT /api/restaurant/{restaurantId}/dishes/{dishId}` – Update dish details.
- `DELETE /api/restaurant/{restaurantId}/dishes/{dishId}` – Remove a dish from a restaurant.

### 🔒 **Security & Authorization**
- **Role-Based Access Control (RBAC)** – Assigns permissions for Admins, Owners, and Users.
- **Claim-Based Authorization** – Grants access based on user attributes (e.g., email verification, 2FA).
- **Resource-Based Authorization** – Ensures only restaurant owners can manage their restaurants.

### ✅ **Testing & CI/CD**
- **Unit Testing**: xUnit & Moq.
- **Integration Testing**: Ensures API endpoints function correctly.
- **GitHub Actions**: Automates build, test, and deployment workflows.


