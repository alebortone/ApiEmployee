using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MinhaApi.Application.Services;
using MinhaApi.Auth;
using MinhaApi.Domain.employee.Interface;
using MinhaApi.Infratruture.Data;
using MinhaApi.Infratruture.Repositories;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var secret = builder.Configuration.GetSection("Settings").GetValue<string>("Secret");
var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };

});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ConnectionContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       
    app.UseSwaggerUI();     
}
app.UseCors();
app.UseAuthentication(); 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();