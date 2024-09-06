using GamesWebApp.Data;
using GamesWebApp.GamesEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


/* builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization(); */

var connectionString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddDbContext<GameStoreContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGameEndpoints();
app.MapGenreEndpoints();

await app.MigrateDbAsync();

app.Run();