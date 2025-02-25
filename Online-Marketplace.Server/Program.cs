using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineMarketplace.Server.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("https://localhost:60254") // ✅ Allow the React frontend
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // ✅ Needed if using cookies or auth tokens
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add authentication services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
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
    });



// Register UserDbContext with Windows Authentication
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register OrderDbContext with Windows Authentication
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register ProductDbContext with Windows Authentication
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseCors("AllowReactApp");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
