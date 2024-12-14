using System.Reflection;
using GusPizza.Application;
using GusPizza.Domain;
using GusPizza.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// database config
builder.Services.AddDbContext<AppDBContext>(options => options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 39)),
    b => b.MigrationsAssembly("GusPizza.API")
));

// repository config
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();

// service config
builder.Services.AddScoped<PizzaService>();

// swagger config
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GusPizza API",
        Version = "v1",
        Description = "Api for manage order pizza in GusPizza"
    });
    c.IncludeXmlComments(Assembly.GetExecutingAssembly());
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GusPizza API v1");
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();