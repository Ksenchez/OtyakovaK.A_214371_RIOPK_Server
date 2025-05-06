using Microsoft.EntityFrameworkCore;
using pricing_analyzer_back.Infrasctructure.Context;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                        policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                          });
});
builder.Logging
    .AddSimpleConsole(o =>
    {
        o.TimestampFormat = "HH:mm:ss ";
        o.UseUtcTimestamp = true;
    });
// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=pa.db"));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Авто-миграция
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.MapControllers();

app.Run();
