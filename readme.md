# Basic CRUD for pizza api using .NET

- the project implements modular architecture
- learn how to use controller in developing api using ASP.NET Core
- clone this project and run `dotnet restore`

- run migrations using

  ```bash
  dotnet ef database update --project GusPizza.API
  ```

- undo the migrations

  ```bash
  dotnet ef migrations remove --project GusPizza.API
  ```

- run with

  ```bash
  dotnet run --project GusPizza.API --launch-profile https
  ```

- running with hot reload

  ```bash
  dotnet watch --project GusPizza.API run --launch-profile https
  ```
