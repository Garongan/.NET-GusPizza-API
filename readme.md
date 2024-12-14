# Basic CRUD for pizza api using .NET

- the project implements modular architecture
- learn how to use controller in developing api using ASP.NET Core
- run migrations using

  ```bash
  dotnet ef migrations add InitialCreate --startup-project GusPizza.API
  dotnet ef database update --startup-project GusPizza.API
  ```

- undo the migrations

  ```bash
  dotnet ef migrations remove --startup-project GusPizza.API
  ```

- run with

  ```bash
  dotnet run --porject GusPizza.API
  ```
