using System.Text.Json;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Seed
{

    public static async Task SeedRestaurants(DataContext context)
    {
        var owner = await context.Owners.FirstOrDefaultAsync(x => x.Id == 1);
        var bosnia = await context.Countries.FirstOrDefaultAsync(x => x.Id == 28);

        List<string> cities = ["Doboj", "Sarajevo", "Tuzla", "Zenica", "Neum", "Mostar", "Banja Luka"];
        List<string> names = ["Rupa", "Teatar", "Cinema", "Black and White", "Fabrika", "Firma", "Kapija"];
        Random random = new();
        List<Restaurant> restaurants = [];
        for (int i = 0; i < 30; i++)
        {
            var currency = await context.Currencies.OrderBy(x => Guid.NewGuid()).FirstOrDefaultAsync();
            Restaurant restaurant = new()
            {
                Address = $"Neka adresa {i}",
                City = cities[random.Next(7)],
                Country = bosnia,
                CountryId = 28,
                CurrencyId = currency.Id,
                Currency = currency,
                Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit, convallis fringilla venenatis imperdiet pretium platea id, sociosqu augue magnis sagittis iaculis conubia. Cras nulla porttitor duis a scelerisque id ridiculus urna in aptent morbi, facilisis cubilia lacinia sollicitudin at ultricies natoque commodo sem sapien, convallis egestas dictum ac quisque maecenas dignissim venenatis parturient hendrerit",
                IsActive = random.Next(2) == 0,
                IsDeleted = false,
                OwnerId = 1,
                Owner = owner,
                IsOpen = random.Next(2) == 0,
                Name = $"{names[random.Next(7)]} - {i}",
                PhoneNumber = $"0{random.Next(32691000, 32691999)}",
                PostalCode = random.Next(10000, 99999),
                FacebookUrl = random.Next(2) == 0 ? "https://www.facebook.com/" : null,
                InstagramUrl = random.Next(2) == 0 ? "https://www.instagram.com/" : null,
                WebsiteUrl = random.Next(2) == 0 ? "https://www.google.com/" : null,
                Latitude = random.Next(2) == 0 ? random.NextDouble() * 90 : random.NextDouble() * 90 * -1,
                Longitude = random.Next(2) == 0 ? random.NextDouble() * 180 : random.NextDouble() * 180 * -1,
            };
            restaurants.Add(restaurant);
        }

        context.Restaurants.AddRange(restaurants);
        await context.SaveChangesAsync();
    }

    public static async Task SeedRestaurantImages(DataContext context)
    {
        Random random = new();
        for (int i = 1; i <= 31; i++)
        {
            var restaurant = await context.Restaurants.FirstOrDefaultAsync(x => x.Id == i);
            List<RestaurantImage> restaurantImages = [];
            for (int j = 0; j < 5; j++)
            {
                var randomNum = random.Next(500, 900);
                RestaurantImage restaurantImage = new()
                {
                    ContentType = "image",
                    FullPath = $"https://picsum.photos/{randomNum}/{randomNum}",
                    IsDeleted = false,
                    Name = $"{restaurant.Name}-image",
                    RelativePath = $"https://picsum.photos/{randomNum}/{randomNum}",
                    RestaurantId = restaurant.Id,
                    Restaurant = restaurant,
                    Size = random.NextInt64(70000, 100000),
                    UniqueName = $"{Guid.NewGuid()}-image",
                    Type = j == 0 ? RestaurantImageType.Profile : RestaurantImageType.Gallery,
                    Url = $"https://picsum.photos/{randomNum}/{randomNum}",
                };

                restaurantImages.Add(restaurantImage);
            }

            context.RestaurantImages.AddRange(restaurantImages);
            await context.SaveChangesAsync();
        }
    }


    public static async Task SeedCountries(DataContext context)
    {
        if (await context.Countries.AnyAsync()) return;

        var countryData = await File.ReadAllTextAsync("Infrastructure/Data/countries.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var countries = JsonSerializer.Deserialize<List<CreateCountryDto>>(countryData, options);

        foreach (var countryDto in countries)
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
        if (await context.Currencies.AnyAsync()) return;

        var currencyData = await File.ReadAllTextAsync("Infrastructure/Data/currency.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var currencies = JsonSerializer.Deserialize<List<CreateCountryDto>>(currencyData, options);

        foreach (var currencyDto in currencies)
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
