***

# AGENTS.md

Guidance for coding agents working on the **PaulAlarba** portfolio repository.

## Project Overview

PaulAlarba is a professional portfolio website built using the **ASP.NET Core MVC** framework. 

- **Backend:** C# / .NET 8+ (MVC Pattern).
- **Frontend:** Razor Views, HTML5, CSS3, and Tailwind.
- **Data Layer:** Entity Framework Core (EF Core) for managing projects, testimonials, and contact messages.
- **Database:** [Insert Database, e.g., SQL Server / SQLite / PostgreSQL].

## Common Commands

All commands should be run from the project root where the `.csproj` or `.sln` file is located.

**General Development:**
```powershell
dotnet restore       # Restore dependencies
dotnet build         # Build the project
dotnet run           # Run the application
dotnet watch         # Run with hot-reload for UI changes
```

**Database & Migrations (EF Core):**
```powershell
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Environment & Configuration

Configuration is handled via `appsettings.json` and `appsettings.Development.json`.

- **Connection Strings:** Located in `ConnectionStrings` section of `appsettings.json`.
- **API Keys:** (e.g., Email service/SendGrid) should be stored in User Secrets during development:
  ```powershell
  dotnet user-secrets set "EmailSettings:ApiKey" "your-key-here"
  ```

**Constraint:** Do not commit real secrets or passwords to the repository. Use placeholders in `appsettings.json`.

## Architecture Notes

### Backend (MVC)
- **Models:** Found in `/Models`. This includes both Database Entities and ViewModels (DTOs for the UI).
- **Views:** Found in `/Views`. Follow the `Views/[ControllerName]/[Action].cshtml` convention.
- **Controllers:** Found in `/Controllers`. Logic should be kept slim; complex business logic should be moved to a `/Services` folder.
- **Data Access:** The `DbContext` is located in `/Data`. Use the Repository pattern if implemented; otherwise, use the DbContext directly in services.
- **Middleware:** Custom middleware or configuration for the request pipeline is located in `Program.cs`.

### Frontend (Static Assets)
- **Static Files:** All CSS, JS, and Images reside in `/wwwroot`.
- **Layouts:** The primary shared layout is `Views/Shared/_Layout.cshtml`.
- **Partial Views:** Reusable UI components (like a project card or navbar) are stored in `Views/Shared/`.

## Coding Conventions

- **C# Standards:** Use **PascalCase** for classes, methods, and properties. Use **camelCase** for local variables and private fields (prefixed with `_`).
- **Async Programming:** Use `async` and `await` for all I/O bound operations (Database calls, API requests) to prevent thread blocking.
- **View Logic:** Keep `.cshtml` files clean. Avoid complex C# logic inside Views; move logic to the Controller or a ViewModel.
- **Dependency Injection:** Always use constructor injection for services and database contexts. Do not manually instantiate services.
- **Naming:** Name your controllers with the `Controller` suffix (e.g., `ProjectController.cs`).

## UI Guidance

- **Styling:** [Insert instruction: e.g., "Use Bootstrap 5 utility classes for layout and custom CSS in `wwwroot/css/site.css` for branding"].
- **Responsiveness:** Ensure all new components are mobile-responsive.
- **Icons:** Use [Insert Icon Library, e.g., FontAwesome or Lucide] for iconography.
- **Assets:** Optimize images before adding them to `wwwroot/images` to maintain fast load times.

## Testing And Verification

Before handing off a task, ensure the following:

- **Compilation:** The project must compile without errors (`dotnet build`).
- **Runtime:** The specific feature must be verified via `dotnet run`.
- **Migrations:** If the database schema changed, a migration must be created and applied.
- **Frontend:** Verify that the page renders correctly across different screen sizes.

## Git And File Hygiene

- **Ignore Files:** Do not commit `/bin`, `/obj`, `.user`, or `.vs` folders.
- **Dependencies:** Update the `.csproj` file correctly when adding NuGet packages.
- **Preservation:** Do not overwrite existing CSS overrides in `site.css` unless explicitly requested.
- **Commits:** Keep commits atomic and focused on a single feature or fix.