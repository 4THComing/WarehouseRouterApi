using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using warehouseRouter.Warehouse.Data;
using warehouseRouter.Warehouse.Interfaces;
using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.Repositories;
using warehouseRouter.Warehouse.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddDbContext<WarehouseDbContext>(opt => 
    opt.UseInMemoryDatabase("warehouseDbContext"));
    builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
    builder.Services.AddScoped<IwarehouseServices, WarehouseService>();
builder.Services.AddScoped<IPasswordHasher<WarehouseDirectory>, PasswordHasher<WarehouseDirectory>>();

var app = builder.Build();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
;

app.Run();

