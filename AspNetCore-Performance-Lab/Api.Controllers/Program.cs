using Api.Controllers.Data;
using Api.Controllers.Repositories.Interfaces;
using Api.Controllers.Services;
using Microsoft.EntityFrameworkCore;
using Api.Controllers.Middleware;
using Api.Controllers.Extensions;
using Api.Controllers.Settings;

//using Api.Controllers
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
builder.Services.AddApplicationService();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

//builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();



app.Run();
