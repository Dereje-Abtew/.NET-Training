
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{

    const string GetGameEndpointName = "GetGame";
    public static void MapGameEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/games");

        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext)
            => await dbContext.Games
                              .Include(game => game.Genre)
                              .Select(game => new GameSummaryDto(
                                game.Id,
                                game.Name,
                                game.Genre!.Name,
                                game.Price,
                                game.DateReleased
                              ))
                              .AsNoTracking()
                              .ToListAsync());


        //GET /games/{id}
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            // to check un-existing list
            var game = await dbContext.Games.FindAsync(id);  // to fetch data from database using FindAsync method and pass id as parameter
            // return game is null ? Results.NotFound() : Results.Ok(game);  yhienn endale bnadergew mulu bemulu ke database yalewn data mestet yhonal slezih DTO enday yderegal malet new.
            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.DateReleased
                )
            );

        })
           .WithName(GetGameEndpointName);



        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            //  for Dependency injection conecpt to add database support for API endpoints, then add class on endpoint that is GameStoreContext

            Game game = new()
            {
                Name = newGame.name,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                DateReleased = newGame.DateReleased
            };
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();//add Async 

            GameDetailsDto gameDto = new(
                game.Id,
                game.Name,
                game.GenreId,
                game.Price,
                game.DateReleased

            );
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);
        });

        // PUT /games/1
        group.MapPut("/{id}", async (int id, UpdateGameDto  updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            if (existingGame is null) return Results.NotFound();
            existingGame.Name = updatedGame.Name;
            existingGame.GenreId = updatedGame.GenreId;
            existingGame.Price = updatedGame.Price;
            existingGame.DateReleased = updatedGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });


        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
    }
}
