using GameStore.Frontend;
using GameStore.Frontend.Client;
using GameStore.Frontend.Components;
using GameStore.Frontend.Data;
using GameStore.Frontend.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveWebAssemblyComponents()
	.AddInteractiveServerComponents();

builder.Configuration.AddEnvironmentVariables(prefix: "GAMESTORE_");
var gameStoreApiUrl = builder.Configuration["GameStoreApiUrl"] ?? throw new Exception("GameStoreURL configuration is required");

builder.Services.AddHttpClient<GamesClient>(client => client.BaseAddress = new Uri(gameStoreApiUrl));
builder.Services.AddHttpClient<GenreClient>(client => client.BaseAddress = new Uri(gameStoreApiUrl));

var dbStringBuilder = new NpgsqlConnectionStringBuilder(builder.Configuration.GetConnectionString("GameStoreIdentity"));
dbStringBuilder.Username = builder.Configuration["DbUser"];
dbStringBuilder.Password = builder.Configuration["DbPassword"];
string connectionString = dbStringBuilder.ConnectionString;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseNpgsql(connectionString));

builder.Services.AddIdentity<GameStoreUser, GameStoreRole>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
		options.SignIn.RequireConfirmedEmail = false;
		options.SignIn.RequireConfirmedPhoneNumber = false;
		options.Password.RequireNonAlphanumeric = false;
	}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddLogging(loggingBuilder => loggingBuilder
	.AddConsole()
	.AddDebug().SetMinimumLevel(LogLevel.Information));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
else
{
	app.UseDeveloperExceptionPage();
}

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode().AddInteractiveWebAssemblyRenderMode();

app.Run();
