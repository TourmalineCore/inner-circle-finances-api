using SalaryService.Application;
using SalaryService.DataAccess;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var configuration = builder.Configuration;

builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
