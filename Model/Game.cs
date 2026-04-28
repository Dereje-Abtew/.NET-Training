namespace GameStore.Api.Model;

public class Game
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public Genre? Genre { get; set; }

 // Foreign key property for Genre,  used to connect the Game to its Genre in the database
    public int GenreId { get; set; }

    public decimal price { get; set; }

    public DateOnly DateReleased { get; set; }
}
