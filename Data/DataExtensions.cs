using GameStore.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{

    // bezih MigrateDb amakagninet tesaten enkawan database bnatefa berasu seat application start snaderg create yderegal
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
                             .GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }



    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("GameStore"); //this "GameStore" is the name of the connection string in appsettings.json
        //DbContext has a scope services liftime b/c
        // it insure that a new instance of DbContext is created per request
        //  kezih lay meten create yaderegnew scope ke dependancy injection gar yeteyayaze new.
        // group.MapPost("/", (CreateGameDto newGame, GameStoreContext DbContext) => endpoint lay yeseranew malet new


        
        builder.Services.AddSqlite<GameStoreContext>(
            connString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                // ENZIH GENRE TABLE SEEDING NACHEW BE DEFUALT ENDNOR LEMADREG YETESERA NEW
                if (!context.Set<Genre>().Any())
                {
                    context.Set<Genre>().AddRange(
                        new Genre { Name = "Fighting" },
                        new Genre { Name = "Football" },
                        new Genre { Name = "RPG" },
                        new Genre { Name = "Strategy" },
                        new Genre { Name = "Sports" }
                    );
                }
                context.SaveChanges();
            })
            );
    }


}
