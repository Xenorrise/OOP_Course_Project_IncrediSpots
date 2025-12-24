using IncrediSpots.App.Interfaces;
using IncrediSpots.App.Services;
using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Interfaces;
using KnowledgeApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddControllers();
services.AddDbContext<IncrediSpotsMainDbContext>(options => 
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<ISpotService, SpotService>();
services.AddScoped<ISpotRepository, SpotRepository>();
services.AddScoped<ISpotCategoryService, SpotCategoryService>();
services.AddScoped<ISpotCategoryRepository, SpotCategoryRepository>();

var app = builder.Build();
app.MapControllers();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.Run();
