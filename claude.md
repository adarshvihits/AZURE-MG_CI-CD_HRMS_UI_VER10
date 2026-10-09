# Project Development Rules

## Project

This is a .NET 8 ASP.NET Core application.

The project follows a layered architecture and existing project conventions must be followed.

Do not introduce a new architecture or coding pattern unless explicitly requested.

---

# Architecture

For a new form/feature, follow this general flow:

UI/View
↓
JavaScript
↓
Controller
↓
Interface
↓
Service
↓
Repository/API/Database

Use the existing project implementation as the primary reference.

---

# Creating a New Form

Whenever I ask you to create a new form, inspect an existing similar form first.

Use the existing form as a template for:

* Folder structure
* Naming conventions
* Controller structure
* Interface structure
* Service structure
* Model structure
* JavaScript structure
* Razor/View structure
* API calls
* Validation
* Error handling
* DataTables
* Dependency Injection
* Response handling

Do not create files using a completely different pattern.

---

# Required Components

For a normal CRUD form, check whether the following components are required:

1. Model
2. Interface
3. Service
4. Controller
5. Razor View
6. JavaScript file
7. API/Repository changes if required
8. Stored Procedure changes if required
9. Dependency Injection registration if required

Only create components that are actually required by the existing architecture.

---

# Controller Rules

Controllers must follow the existing controller pattern.

Before creating a controller:

1. Inspect an existing controller from the same module.
2. Follow its namespace.
3. Follow its constructor dependency injection pattern.
4. Follow its action naming convention.
5. Follow existing response handling.

Example pattern:

```csharp
private readonly IEmployeeService _service;

public EmployeeController(IEmployeeService service)
{
    _service = service;
}
```

Do not instantiate services directly inside controllers.

Do NOT do this:

```csharp
private EmployeeService _service = new EmployeeService();
```

Use dependency injection.

---

# Interface Rules

Interfaces must follow the existing IService structure.

Example:

```csharp
public interface IEmployeeService
{
    Task<ResponseModel> GetEmployee(...);
    Task<ResponseModel> SaveEmployee(...);
}
```

Before creating an interface, inspect an existing interface and follow its method naming, return types and parameter conventions.

---

# Service Rules

Services must implement their corresponding interface.

Example:

```csharp
public class EmployeeService : IEmployeeService
{
}
```

Follow the existing project pattern for:

* HttpService
* GetAsync
* PostAsync
* JWT/session handling
* Request models
* Response models
* Error handling

Do not introduce a new HTTP/API pattern if an existing pattern is available.

---

# Model Rules

Models must follow existing project naming conventions.

Before creating a model:

1. Inspect similar models.
2. Follow existing property naming.
3. Use the same data types used elsewhere in the project.
4. Use existing validation attributes where applicable.

Do not unnecessarily add new properties or frameworks.

---

# JavaScript Rules

For every form requiring JavaScript:

1. Inspect an existing similar JS file.
2. Follow the same naming convention.
3. Follow the existing AJAX/API calling pattern.
4. Follow the existing validation pattern.
5. Follow existing DataTable implementation.
6. Follow existing success/error message handling.
7. Follow existing date picker and UI components.

Do not introduce a new JavaScript framework unless explicitly requested.

---

# Razor/View Rules

Views must follow the existing project's Razor/MVC structure.

Inspect a similar existing view before creating a new one.

Follow existing:

* HTML structure
* Metronic components
* Form controls
* Date pickers
* Dropdowns
* Validation
* Buttons
* Modal structure
* DataTables
* Partial views

---

# Dependency Injection

When a new service/interface is created:

1. Check how existing services are registered.
2. Register the new service using the same pattern.
3. Do not change existing registrations unnecessarily.

For example, if the project uses:

```csharp
services.AddTransient<IEmployeeService, EmployeeService>();
```

follow that pattern.

---

# API Calls

Use the existing project HTTP service/repository architecture.

If existing code uses:

```csharp
_httpService.GetAsync(...)
```

or:

```csharp
_httpService.PostAsync(...)
```

follow that pattern.

Do not replace it with HttpClient directly unless explicitly requested.

---

# Naming

Follow existing project naming conventions.

For example:

EmployeeBankDetails

should normally produce appropriately named files such as:

EmployeeBankDetailsController.cs
IEmployeeBankDetailsService.cs
EmployeeBankDetailsService.cs
EmployeeBankDetailsModel.cs
employee-bank-details.js

However, first inspect the existing project because the actual naming convention takes priority over this example.

---

# Existing Code First

Before creating a new feature:

1. Search the project for similar functionality.
2. Identify the closest existing form.
3. Inspect its Controller.
4. Inspect its Interface.
5. Inspect its Service.
6. Inspect its Model.
7. Inspect its View.
8. Inspect its JavaScript.
9. Reuse the established patterns.

Existing project code is the source of truth.

---

# Database

If the feature requires database changes:

1. Inspect existing stored procedure conventions.
2. Follow existing naming conventions.
3. Follow existing parameter naming.
4. Follow existing response/status conventions.
5. Do not modify unrelated stored procedures.

---

# Before Finishing

After creating a feature:

1. Check all namespaces.
2. Check all using statements.
3. Check interface/service method signatures.
4. Check dependency injection.
5. Check controller dependencies.
6. Check JavaScript references.
7. Check Razor View references.
8. Check API endpoints.
9. Check model property names.
10. Build the project if possible.
11. Fix compilation errors caused by the changes.

Do not claim the feature is complete if there are known compilation errors.

---

# Important Rule

When I say:

"Create a new form"

do not ask me to repeat the architecture unless something is genuinely unclear.

Automatically follow these project rules and inspect the existing code to determine the correct implementation.

When there are multiple possible implementations, prefer the implementation already used elsewhere in this project.
