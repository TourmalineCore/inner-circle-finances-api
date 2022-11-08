using SalaryService.Application;
using SalaryService.DataAccess;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddPersistence();

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
