namespace API;

public class TableCardDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TableRestaurant Restaurant { get; set; }
}

public class TableRestaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
}
