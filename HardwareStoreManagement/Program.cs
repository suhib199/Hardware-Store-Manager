using HardwareStoreRepository.CacheServicesImplementation;
using HardwareStoreRepository.DBContext;
using HardwareStoreRepository.DTO;
using HardwareStoreRepository.Helper;
using HardwareStoreRepository.Helper.CacheService;
using HardwareStoreRepository.Models;
using HardwareStoreRepository.Repo.Customer;
using HardwareStoreRepository.Repo.Employee;
using HardwareStoreRepository.Repo.Invoice;
using HardwareStoreRepository.Repo.Products;
using HardwareStoreRepository.Repo.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Hardware Store Management",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<HardwareStoreDBContext>(cnn => cnn.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddScoped<IProductRepository, ProductImplement>();
builder.Services.AddScoped<IInvoiceRepossitory, InvoiceImplement>();
builder.Services.AddScoped<ICustomerRepository, CustomerImplement>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeImplement>();
builder.Services.AddScoped<IRedisCacheService, RedisCacheService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JWTService>();




var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string loggerPath = configuration.GetSection("LoggerPath").Value;
Serilog.Log.Logger = new Serilog.LoggerConfiguration().ReadFrom.Configuration(configuration).
                WriteTo.File(loggerPath, rollingInterval: RollingInterval.Day).
                CreateLogger();

var app = builder.Build();
//builder.Services.AddScoped<ProductInterface,ProductImplementation>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
