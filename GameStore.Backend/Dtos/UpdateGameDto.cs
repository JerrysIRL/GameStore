using System.ComponentModel.DataAnnotations;

namespace GamesWebApp.Dtos;

public record UpdateGameDto(
    [Required][StringLength(50)]string Name,
    int GenreId,
    [Range(1,100)] decimal Price,
    DateOnly ReleaseDate
    );