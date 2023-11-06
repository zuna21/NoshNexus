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


    public static async Task SeedCurrency(DataContext context)
    {
        if(await context.Currencies.AnyAsync()) return;

        var currencyData = await File.ReadAllTextAsync("Infrastructure/Data/currency.json");
        var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        var currencies = JsonSerializer.Deserialize<List<CreateCountryDto>>(currencyData, options);

        foreach(var currencyDto in currencies)
        {
            var currency = new Currency
            {
                Name = currencyDto.Name,
                Code = currencyDto.Code
            };
            context.Currencies.Add(currency);
        }

        await context.SaveChangesAsync();
    }

}
