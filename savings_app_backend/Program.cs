using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using savings_app_backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using savings_app_backend.Services.Interfaces;
using savings_app_backend.Services.Implementations;
using savings_app_backend.EmailSender;
using NLog.Web;
using NLog;
using savings_app_backend;
using savings_app_backend.Statistics;
using savings_app_backend.Repositories.Interfaces;
using savings_app_backend.Repositories.Implementations;
using savings_app_backend.SavingToFile;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

    builder.Services.AddDbContext<SavingsAppContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("savingsAppContext") ?? throw new InvalidOperationException("Connection string 'savingsAppContext' not found.")));



    // Add services to the container.

    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    builder.Services.AddSingleton<IFileSaver, FileSaver>();
    builder.Services.AddTransient<IRestaurantService, RestaurantService>();
    builder.Services.AddTransient<IProductService, ProductService>();

    builder.Services.AddTransient<IPickupService, PickupService>();

    builder.Services.AddTransient<IOrderService, OrderService>();

    builder.Services.AddTransient<IBuyerService, BuyerService>();

    //builder.Services.AddTransient<IAddressService, AddressService>();

    builder.Services.AddTransient<IUserAuthService, UserAuthService>();

    builder.Services.AddTransient<IEmailSender, EmailSender>();

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
    builder.Services.AddScoped<IUserAuthRepository, UserAuthRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IPickupRepository, PickupRepository>();
    builder.Services.AddScoped<IBuyerRepository, BuyerRepository>();

    //builder.Logging.ClearProviders();
    builder.Host.UseNLog();


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

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
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

    app.UseLoggingMiddleware();
    app.UseStatisticsMiddleware();

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseCors(MyAllowSpecificOrigins);

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();


    app.Run();
}
catch(Exception e)
{
    logger.Error(e);
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}