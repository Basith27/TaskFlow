using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;
using TaskFlow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using TaskFlow.Hubs; // Add this line

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Add controllers for REST API
builder.Services.AddSignalR(); // Add SignalR services

// Add service layer classes for dependency injection
builder.Services.AddScoped<TaskFlow.Services.WorkspaceService>();
builder.Services.AddScoped<TaskFlow.Services.BoardService>();
builder.Services.AddScoped<TaskFlow.Services.ListService>();
builder.Services.AddScoped<TaskFlow.Services.CardService>();
builder.Services.AddScoped<TaskFlow.Services.CommentService>();
builder.Services.AddScoped<TaskFlow.Services.UserService>();

// Register repositories for dependency injection
builder.Services.AddScoped<TaskFlow.Repositories.IWorkspaceRepository, TaskFlow.Repositories.WorkspaceRepository>();
builder.Services.AddScoped<TaskFlow.Repositories.IBoardRepository, TaskFlow.Repositories.BoardRepository>();
builder.Services.AddScoped<TaskFlow.Repositories.IListRepository, TaskFlow.Repositories.ListRepository>();
builder.Services.AddScoped<TaskFlow.Repositories.ICardRepository, TaskFlow.Repositories.CardRepository>();
builder.Services.AddScoped<TaskFlow.Repositories.ICommentRepository, TaskFlow.Repositories.CommentRepository>();
builder.Services.AddScoped<TaskFlow.Repositories.IUserRepository, TaskFlow.Repositories.UserRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Explicitly add console logging for debugging
builder.Logging.AddConsole();

// Configure DbContext with MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21)))); // Specify your MySQL server version here

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; // For development only, set to true in production
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT Secret is not configured.")))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization(); // Add authorization middleware

app.MapControllers(); // Map controllers for REST API
app.MapHub<TaskFlowHub>("/taskflowhub"); // Map SignalR Hub

app.Run();
