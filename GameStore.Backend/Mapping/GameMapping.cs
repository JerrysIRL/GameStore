using GamesWebApp.Dtos;
using GamesWebApp.Entities;

namespace GamesWebApp.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto gameDto)
    {
        return new Game()
        {
            Name = gameDto.Name,
            GenreId = gameDto.GenreId,
            Price = gameDto.Price,
            ReleaseDate = gameDto.ReleaseDate
        };
    }

    public static Game ToEntity(this UpdateGameDto updateGame, int id)
    {
        return new Game()
        {
            Id = id,
            Name = updateGame.Name,
            GenreId = updateGame.GenreId,
            Price = updateGame.Price,
            ReleaseDate = updateGame.ReleaseDate
        };
    }

    public static GameSummaryDto ToSummaryDto(this Game game)
    {
        return new GameSummaryDto(
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }

    public static GameDetailsDto ToDetailsDto(this Game game)
    {
        return new GameDetailsDto(
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }


}