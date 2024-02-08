namespace ApplicationCore.DTOs.OwnerDtos;

public class TableDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class TableCardDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public GetRestaurantTableDto Restaurant { get; set; }
}

public class GetRestaurantTableDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
