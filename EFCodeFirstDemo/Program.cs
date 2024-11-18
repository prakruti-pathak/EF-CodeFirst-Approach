using EFCodeFirstDemo.Data;
using EFCodeFirstDemo.Data.Contract;
using EFCodeFirstDemo.Data.Implementation;
using EFCodeFirstDemo.Services.Contract;
using EFCodeFirstDemo.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

//Dependency Injection
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// Add services to the container.

builder.Services.AddControllers();

//database connection
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("mydb"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.Run();
