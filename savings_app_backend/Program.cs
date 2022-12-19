using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web;
using NLog;
using Infrastructure;
using Domain.Interfaces;
using Infrastructure.Services;
using Application.Services.Interfaces;
using Application.Services.Implementations;
using Domain.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Diagnostics;
using WebAPI.Middlewares;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Application.Interceptors;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    
    LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

    builder.Services.AddDbContext<SavingsAppContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("savingsAppContext") ?? throw new InvalidOperationException("Connection string 'savingsAppContext' not found.")));

    builder.Services.AddScoped<IdentityInterceptor>();
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureContainer<ContainerBuilder>(builder =>
        {
            builder.RegisterType<BuyerService>().As<IBuyerService>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(IdentityInterceptor))
            .InstancePerDependency();
        });

    // Add services to the container.

    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    builder.Services.AddScoped<IFileSaver, FileSaver>();
    builder.Services.AddScoped<IRestaurantService, RestaurantService>();
    builder.Services.AddScoped<IProductService, ProductService>();

    builder.Services.AddScoped<IPickupService, PickupService>();

    builder.Services.AddScoped<IOrderService, OrderService>();

    builder.Services.AddScoped<IShopService, ShopService>();

    builder.Services.AddScoped<IBuyerService, BuyerService>();

    builder.Services.AddScoped<IAuthService, AuthService>();

    builder.Services.AddScoped<IEmailSender, EmailSender>();

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IPickupRepository, PickupRepository>();
    builder.Services.AddScoped<IBuyerRepository, BuyerRepository>();
    

    //builder.Logging.ClearProviders();


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

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<MyExceptionHandlerMiddleware>();

    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("en-US")
    });

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