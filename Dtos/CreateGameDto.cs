using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record CreateGameDto(
    [Required][StringLength(50)] string name,
    [Required][StringLength(20)] string Geners,
    [Range(1,100)] decimal price,
    DateOnly ReleaseDate

);
