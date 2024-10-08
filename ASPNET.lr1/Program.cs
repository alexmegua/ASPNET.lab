using MyCalcApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<CalcService>();
builder.Services.AddTransient<TimeService>();

builder.Services.AddControllers();

var app = builder.Build();

// маршрути
app.MapControllers();

app.Run();
