namespace ApplicationCore;

public class RestaurantReviewDto
{
    public int Id { get; set; }
    public RestaurantReviewCustomerDto Customer { get; set; }
    public float Rating { get; set; }
    public string Review { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class RestaurantReviewCustomerDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string ProfileImage { get; set; }
}

public class CreateReviewDto
{
    public float Rating { get; set; }
    public string Review { get; set; }
}