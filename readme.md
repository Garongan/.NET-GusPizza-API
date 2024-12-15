# Basic CRUD for pizza api using .NET

- the project implements DDD (Domain Driven Design) architecture
- learn how to use controller in developing api using ASP.NET Core
- learn how to documented API with Swashbuckle Swagger
- learn how to create jwt auth using dotnet
- learn how to authorize user by role
- create basic CRUD operation for pizza endpoint

# How to run this API locally

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
