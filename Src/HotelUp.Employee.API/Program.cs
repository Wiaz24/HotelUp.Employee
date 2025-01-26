using System.Text.Json.Serialization;
using HotelUp.Employee.API.Cors;
using HotelUp.Employee.API.Swagger;
using HotelUp.Employee.Persistence;
using HotelUp.Employee.Services;
using HotelUp.Employee.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddShared();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddCustomSwagger(builder.Configuration);
builder.Services.AddCorsForFrontend(builder.Configuration);
builder.Services.AddServiceLayer();
builder.Services.AddPersistenceLayer();

var app = builder.Build();

app.UseShared();
app.UseCustomSwagger();
app.UseCorsForFrontend();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/api/employee/swagger/index.html"))
    .Produces(200)
    .ExcludeFromDescription();
app.Run();

public interface IApiMarker
{
}