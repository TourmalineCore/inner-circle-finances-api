using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.EventLog;
using SalaryService.Application;
using SalaryService.DataAccess;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.OpenApi.Models;
using SalaryService.Api;
using TourmalineCore.AspNetCore.JwtAuthentication.Core;
using TourmalineCore.AspNetCore.JwtAuthentication.Core.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("SalarySpecificOrigins",
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyHeader().AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
var configuration = builder.Configuration;

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;

    var reloadOnChange = hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", true);

    config.AddJsonFile("appsettings.json", true, reloadOnChange)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, reloadOnChange)
        .AddJsonFile("appsettings.Active.json", true, reloadOnChange);

    if (env.IsDevelopment() && !string.IsNullOrEmpty(env.ApplicationName))
    {
        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));

        config.AddUserSecrets(appAssembly, true);
    }

    config.AddEnvironmentVariables();

    if (args != null)
    {
        config.AddCommandLine(args);
    }
});

var authenticationOptions = configuration.GetSection(nameof(AuthenticationOptions)).Get<AuthenticationOptions>();
builder.Services.AddJwtAuthentication(authenticationOptions).WithUserClaimsProvider<UserClaimsProvider>(UserClaimsProvider.PermissionClaimType); 
builder.Services.AddPersistence(configuration);

builder.Host.ConfigureLogging((hostingContext, logging) =>
{
    var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    // IMPORTANT: This needs to be added *before* configuration is loaded, this lets
    // the defaults be overridden by the configuration.
    if (isWindows)
    {
        // Default the EventLogLoggerProvider to warning or above
        logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning);
    }

    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    logging.AddConsole();
    logging.AddDebug();
    logging.AddEventSourceLogger();

    if (isWindows)
    {
        // Add the EventLogLoggerProvider on windows machines
        logging.AddEventLog();
    }

    logging.Configure(options =>
    {
        options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                          | ActivityTrackingOptions.TraceId
                                          | ActivityTrackingOptions.ParentId;
    }
        );
});

builder.Services.AddApplication(configuration);
builder.Services.AddPersistence(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.ConfigureExceptionHandler();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
    await context.Database.MigrateAsync();
}

app.UseRouting();

app.UseCors("SalarySpecificOrigins");

app.UseJwtAuthentication();

app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireCors("SalarySpecificOrigins"); });

app.Run();
