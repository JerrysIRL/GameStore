using System.Security.Claims;
using GamesWebApp.Data;
using GamesWebApp.Dtos;
using GamesWebApp.Entities;
using GamesWebApp.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GamesWebApp.GamesEndpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameSummaryDto> Games =
    [
        new(1, "Street Fighter II", "shit", 49.99m, new DateOnly(2019, 8, 27)),
        new(2, "Rocket League", "shit", 19.99m, new DateOnly(2016, 5, 15)),
        new(3, "Counter Strike", "shit", 0, new DateOnly(2023, 5, 15))
    ];

    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("games");

        group.MapGet("/", async (GameStoreContext dbContext, ClaimsPrincipal user) =>
        {
            return await dbContext.Games.Include(game => game.Genre).OrderBy(key => key.Id).Select(game => game.ToSummaryDto()).AsNoTracking().ToListAsync();
        });


        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            Game? game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(game.ToDetailsDto());
        }).WithName(GetGameEndpointName);


        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = newGame.ToEntity();

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDetailsDto = game.ToDetailsDto();

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDetailsDto);
        }).WithParameterValidation();

        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if(existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Games.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();

            return Results.Ok();
        });

        group.RequireAuthorization(policy =>
        {
            policy.RequireRole("admin");
        });

        return group;
    }
}