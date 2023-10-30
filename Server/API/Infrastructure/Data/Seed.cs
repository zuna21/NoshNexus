using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Seed
{
    public static async Task SeedCountries(DataContext context)
    {
        if(await context.Countries.AnyAsync()) return;

        var countryData = await File.ReadAllTextAsync("Infrastructure/Data/countries.json");
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        var countries = JsonSerializer.Deserialize<List<CreateCountryDto>>(countryData, options);

        foreach(var countryDto in countries)
        {
            var country = new Country
            {
                Name = countryDto.Name,
                Code = countryDto.Code
            };
            context.Countries.Add(country);
        }

        await context.SaveChangesAsync();
    }

}
