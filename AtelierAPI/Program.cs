using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AtelierAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQLite
builder.Services.AddDbContext<AtelierContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5199") // Allow Blazor app origin (AtelierBlazorApp folder)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Validate JWT configuration
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key is not configured in appsettings.json or appsettings.Development.json.");
}

var jwtIssuer = builder.Configuration["Jwt:Issuer"];
if (string.IsNullOrEmpty(jwtIssuer))
{
    throw new InvalidOperationException("JWT Issuer is not configured in appsettings.json or appsettings.Development.json.");
}

var jwtAudience = builder.Configuration["Jwt:Audience"];
if (string.IsNullOrEmpty(jwtAudience))
{
    throw new InvalidOperationException("JWT Audience is not configured in appsettings.json or appsettings.Development.json.");
}

// Log loaded configuration for debugging
Console.WriteLine($"Jwt:Key: {jwtKey}");
Console.WriteLine($"Jwt:Issuer: {jwtIssuer}");
Console.WriteLine($"Jwt:Audience: {jwtAudience}");

// Add JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
     options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine($"Token validated for: {context.Principal.Identity.Name}");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine($"Authentication challenge: {context.AuthenticateFailure?.Message}");
            return Task.CompletedTask;
        }
    };
});

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS
app.UseCors("AllowSpecificOrigins");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.Use(async (context, next) =>
{
    var token = context.Request.Headers["Authorization"];
    Console.WriteLine($"Authorization Header: {token}");
    await next();
});

app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
