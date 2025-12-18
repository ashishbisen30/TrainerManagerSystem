// Program.cs
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainerManager.Application;
using TrainerManager.Infrastructure.Data;
using TrainerManager.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// SWAGGER SERVICES ADDED HERE
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); ;
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddDbContext<TrainerDbContext>(opt => opt.UseSqlite("Data Source=Trainers.db"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TrainerManager.Application.Features.Trainers.Commands.CreateTrainerCommand).Assembly));
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();   // Generates the JSON spec
    app.UseSwaggerUI(); // Generates the Web Page UI
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
