namespace GameStore.Api.Dtos;
//  a shared agreement about how data will be tranfered and share.
public record GameDto(
    int Id,
    string Name,
    string Genres,
    decimal Price,
    DateOnly ReleaseDate
);