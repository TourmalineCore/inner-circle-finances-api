using Microsoft.EntityFrameworkCore;
using SalaryService.Application;
using SalaryService.Application.Services;
using SalaryService.DataAccess;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configuration = builder.Configuration;


builder.Services.AddApplication(configuration);
builder.Services.AddPersistence(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
await app.Services.GetRequiredService<FinanceAnalyticService>().CalculateTotalFinances();

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

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
