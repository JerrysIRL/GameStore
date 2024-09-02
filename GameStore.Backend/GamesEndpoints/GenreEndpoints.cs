using GamesWebApp.Data;
using GamesWebApp.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GamesWebApp.GamesEndpoints;

public static class GenreEndpoints
{
    const string GetGenreEndpointName = "GetGenre";

    public static RouteGroupBuilder MapGenreEndpoints(this WebApplication app)
    {
        RouteGroupBuilder group = app.MapGroup("genres");

        group.MapGet("/", async (GameStoreContext dbContext)
            => await dbContext.Genres.OrderBy(key => key.Id).Select(genre => genre.ToDto()).AsNoTracking().ToListAsync());


        return group;
    }
}