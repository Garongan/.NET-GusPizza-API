using GusPizza.Application;
using GusPizza.Domain;
using GusPizza.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 39)),
    b => b.MigrationsAssembly("GusPizza.API")
));

builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();

builder.Services.AddScoped<PizzaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.MapControllers();

app.Run();