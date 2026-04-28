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

}
