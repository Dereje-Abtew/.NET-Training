
using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();
app.MapGameEndpoints();  // call refactored method to map endpoints and for better organization of code
app.MigrateDb(); // call the new method to apply any pending migrations to the database and used to ensure that the database schema is up to date with the latest changes defined in the application's data models and migrations. and it is typically called during application startup to ensure that the database is ready to be used before handling any incoming requests.

app.Run();
