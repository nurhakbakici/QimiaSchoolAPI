using Microsoft.EntityFrameworkCore;
using QimiaSchool.DataAccess;
using QimiaSchool.DataAccess.Repositories.Abstractions;
using QimiaSchool.Business.Abstractions;
using QimiaSchool.Business.DependencyInjection;
using QimiaSchool.Business.Implementations;
using QimiaSchool.DataAccess.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using QimiaSchool.DataAccess.MessageBroker.Implementations;
using Microsoft.Extensions.Options;
using MassTransit;
using QimiaSchool.Business.Implementations.Events.Courses;
using QimiaSchool.Common;
using System.Text;
using QimiaSchool.Business.Implementations.Events.Students;
using QimiaSchool.Business.Implementations.Events.Enrollments;
using QimiaSchool.Business.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .WriteTo.File(path: "logs/log.txt")
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        IndexFormat = "dotnetLog",
        AutoRegisterTemplate = true,
        MinimumLogEventLevel = LogEventLevel.Information
    })
    .CreateLogger();



builder.Services.AddDbContext<QimiaSchoolDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options => options.CommandTimeout(120)));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();


builder.Services.Configure<MessageBrokerSettings>(
    builder.Configuration.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<CourseCreatedEventConsumer>();
    busConfigurator.AddConsumer<CourseUpdatedEventConsumer>();
    busConfigurator.AddConsumer<CourseDeletedEventConsumer>();

    busConfigurator.AddConsumer<StudentCreatedEventConsumer>();
    busConfigurator.AddConsumer<StudentUpdatedEventConsumer>();
    busConfigurator.AddConsumer<StudentDeletedEventConsumer>();

    busConfigurator.AddConsumer<EnrollmentCreatedEventConsumer>();
    busConfigurator.AddConsumer<EnrollmentUpdatedEventConsumer>();
    busConfigurator.AddConsumer<EnrollmentDeletedEventConsumer>();


    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.UserName);
            h.Password(settings.Password);
        });

        configurator.ConfigureEndpoints(context);
    });
});



builder.Services.AddBusinessLayer();

// ask this code to bugra
builder.Services.AddLogging(i =>
{
    i.AddConsole();
});


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "QimiaSchool";
});



builder.Services.Configure<Auth0Configuration>(builder.Configuration.GetSection("Auth0"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Auth0:ClientSecret"]));
    options.Authority = $"{builder.Configuration["Auth0:Domain"]}";
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name",
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}",
        ValidAudience = builder.Configuration["Auth0:Audience"],
    };
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Qimia School", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
        Scheme = "bearer",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    };
    c.AddSecurityRequirement(securityRequirement);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<TokenRefreshMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { } // by this we will be able to access our program as a type within factory.