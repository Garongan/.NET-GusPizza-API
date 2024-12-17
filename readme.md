# .NET GusPizza API

- the project implements DDD (Domain Driven Design) architecture
- learn how to use controller in developing api using ASP.NET Core
- learn how to documented API with Swashbuckle Swagger
- learn how to create jwt auth using dotnet
- learn how to authorize user by role
- has two role (Admin and User)
- admin account use `username: admin` and `password: password`
- has authentication using jwt feature like (login account and register user)
- has pizza management feature such like (get all, create, get by id, update, and delete pizza)
- has user management feature such like (get detail of logged user, update detail of logged user, and get all user by admin)
- has transaction feature such like (create new, get all, get by id, and get transaction by logged user)

# How to run this API locally

- clone this project and run

  ```bash
  dotnet restore
  ```

- update your database schema with migrations using

  ```bash
  dotnet ef database update --project GusPizza.Infrastructure --startup-project GusPizza.API
  ```

- run with

  ```bash
  dotnet run --project GusPizza.API --launch-profile https
  ```

- running with hot reload

  ```bash
  dotnet watch --project GusPizza.API run --launch-profile https
  ```

# Additional info

- example how to add migrations

  ```bash
  dotnet ef migrations add [MigrationName] --project GusPizza.Infrastructure --startup-project GusPizza.API
  ```
