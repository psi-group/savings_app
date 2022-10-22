using savings_app_backend.WebSite.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using savings_app_backend.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<savingsAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("savingsAppContext") ?? throw new InvalidOperationException("Connection string 'savingsAppContext' not found.")));

// Add services to the container.

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddTransient<DataAccessServiceProducts>();
builder.Services.AddTransient<DataAccessServiceRestaurants>();
builder.Services.AddTransient<DataAccessServiceOrders>();
builder.Services.AddTransient<DataAccessServicePickups>();
builder.Services.AddTransient<DataAccessServiceUserAuth>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:3000"); // add the allowed origins
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowCredentials();
                      });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
