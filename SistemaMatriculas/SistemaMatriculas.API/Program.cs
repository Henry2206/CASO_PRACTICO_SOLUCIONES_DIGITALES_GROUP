using Microsoft.EntityFrameworkCore;
using Serilog;
using SistemaMatriculas.Application;
using SistemaMatriculas.Application.Interfaces;
using SistemaMatriculas.Infrastructure.Data;
using SistemaMatriculas.Infrastructure.Repositories;


Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/matriculas_log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Aquí se registra el DbContext
builder.Services.AddDbContext<SistemaMatriculasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMatriculasServicio, MatriculasServicio>();
builder.Services.AddScoped<IMatriculasRepositorio, MatriculasRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
