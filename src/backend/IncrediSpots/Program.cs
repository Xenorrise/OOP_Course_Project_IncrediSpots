using System.Text;
using IncrediSpots.App.Interfaces;
using IncrediSpots.App.Services;
using IncrediSpots.DataAccess.Context;
using IncrediSpots.Domain.Interfaces;
using KnowledgeApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddOpenApi();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(options =>
{
    options.AddPolicy("frontend", p =>
        p.WithOrigins("http://localhost:3000")
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials()
    );
});

services.AddControllers();
services.AddDbContext<IncrediSpotsMainDbContext>(options => 
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

services.AddScoped<ISpotService, SpotService>();
services.AddScoped<ISpotRepository, SpotRepository>();
services.AddScoped<ISpotCategoryService, SpotCategoryService>();
services.AddScoped<ISpotCategoryRepository, SpotCategoryRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<ICommentRepository, CommentRepository>();
services.AddScoped<ICommentService, CommentService>();

services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IJwtService, JwtService>();
services.AddScoped<IPasswordService, PasswordService>();


services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

services.AddAuthorization();

var app = builder.Build();

app.UseCors("frontend");

app.UseAuthentication();
app.UseAuthorization();

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
