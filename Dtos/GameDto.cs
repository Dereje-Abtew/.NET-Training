namespace GameStore.Api.Dtos;
//  a shared agreement about how data will be tranfered and share.
public record GameDto(
    int Id,
    string name,
    string Geners,
    decimal Price,
    DateOnly ReleaseDate
);