namespace API;

public class CountryDto
{

}

public class CreateCountryDto
{
    public string Name { get; set; }
    public string Code { get; set; }
}

public class GetCountryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}