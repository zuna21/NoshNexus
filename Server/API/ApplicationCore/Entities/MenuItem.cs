namespace API;

public class MenuItem
{
    public int Id { get; set; }
    public int MenuId  { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public bool HasSpecialOffer { get; set; }
    public double SpecialOfferPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    


    // Navigation properties
    public Menu Menu { get; set; }
}
