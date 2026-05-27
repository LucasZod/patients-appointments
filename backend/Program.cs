using System.Text.Json.Serialization;
using Backend.Modules.Patients;
using Backend.Modules.Queue;
using Backend.Modules.Samples;
using Backend.Modules.ServiceOrders;
using Backend.Shared.Infrastructure;
using Backend.Shared.Presentation.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
           .UseSnakeCaseNamingConvention());

builder.Services.AddPatientsModule();
builder.Services.AddServiceOrdersModule();
builder.Services.AddQueueModule();
builder.Services.AddSamplesModule();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
