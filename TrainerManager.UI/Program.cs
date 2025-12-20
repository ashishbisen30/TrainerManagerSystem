using Microsoft.EntityFrameworkCore; // Added this
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Application.Features.Trainers.Queries;
using TrainerManager.Infrastructure.Data;
using TrainerManager.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Razor Pages
builder.Services.AddRazorPages();

//// 2. Register Database (Requires Microsoft.EntityFrameworkCore.SqlServer package)
//builder.Services.AddDbContext<TrainerDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// 2. Register Database - CHANGED from UseSqlServer to UseSqlite
builder.Services.AddDbContext<TrainerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ... remaini

// 3. Register MediatR (Removed the duplicate)
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(GetTrainersQuery).Assembly);
});

// 4. Register AutoMapper
builder.Services.AddAutoMapper(typeof(TrainerSummaryDto).Assembly);
// Register the File Storage Service
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Note: MapStaticAssets is for .NET 9, UseStaticFiles is standard
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();