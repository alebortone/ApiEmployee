using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MinhaApi.Application.Interfaces;
using MinhaApi.Application.UseCases.Auth.Login;
using MinhaApi.Application.UseCases.Employees.CreateEmployee;
using MinhaApi.Application.UseCases.Employees.DeleteEmployee;
using MinhaApi.Application.UseCases.Employees.GetEmployeeById;
using MinhaApi.Application.UseCases.Employees.GetEmployees;
using MinhaApi.Application.UseCases.Employees.GetPhotoEmployee;
using MinhaApi.Application.UseCases.Employees.UpdateEmployee;
using MinhaApi.Infrastructure.Data;
using MinhaApi.Infrastructure.Repositories;
using MinhaApi.Infrastructure.Security;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
 

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MG Health HIS Api",

        Description = "This Api is part of MG Health HIS.",

        Contact = new OpenApiContact { Name = "<MG/> CODE" }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        Type = SecuritySchemeType.ApiKey,

        Scheme = "Bearer",

        BearerFormat = "JWT",

        In = ParameterLocation.Header,

        Description = "JWT Authorization header using the Bearer scheme"
    });

    //options.EnableAnnotations();

});

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<IFileStorage, FileStorageRepository>();
builder.Services.AddScoped<CreateEmployeeHandler>();
builder.Services.AddScoped<GetEmployeesHandler>();
builder.Services.AddScoped<GetEmployeeByIdHandler>();
builder.Services.AddScoped<DeleteEmployeeHandler>();
builder.Services.AddScoped<UpdateEmployeeHandle>();
builder.Services.AddScoped<GetEmployeePhotoHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateEmployeeValidator>();
builder.Services.AddFluentValidationAutoValidation();

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