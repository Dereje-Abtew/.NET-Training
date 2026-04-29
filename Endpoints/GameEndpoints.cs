
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Model;

namespace GameStore.Api.Endpoints;

public static class GameEndpoints
{

    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
    new(
        1,
        "Streate fighter II",
        "Fighting",
        19.99m,
        new DateOnly(1991, 2, 1)
    ),
    new(
        2,
        "Streate fighter III",
        "Fighting two",
        19.99m,
        new DateOnly(1991, 2, 2)
    ),
    new(
        3,
        "Streate fighter VI",
        "Fighting three",
        19.99m,
        new DateOnly(1991, 2, 3)
    )
];

    public static void MapGameEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("/games");

        // GET /games
        group.MapGet("/", () => games);


        //GET /games/{id}
        group.MapGet("/{id}", (int id) =>
        {
            // to check un-existing list
            var game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
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
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1) return Results.NotFound();
            games[index] = new GameDto(
                id,
                updatedGame.name,
                updatedGame.Geners,
                updatedGame.price,
                updatedGame.ReleaseDate

            );

            return Results.NoContent();
        });


        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
    }
}
