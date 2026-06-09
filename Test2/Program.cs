using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<HotelDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException(
            "Connection string 'DefaultConnection' is not configured.")));
builder.Services.AddScoped<IHotelService, HotelService>();

var app = builder.Build();

app.MapControllers();

app.Run();
