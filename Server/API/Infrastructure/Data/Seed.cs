using System.Text.Json;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace API;

public class Seed()
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
        var restaurantNumber = await context.Restaurants.CountAsync();
        for (int i = 1; i <= restaurantNumber; i++)
        {
            var restaurant = await context.Restaurants.FirstOrDefaultAsync(x => x.Id == i);
            List<RestaurantImage> restaurantImages = [];
            for (int j = 0; j < 5; j++)
            {
                var randomNum = random.Next(500, 900);
                RestaurantImage restaurantImage = new()
                {
                    ContentType = "image",
                    IsDeleted = false,
                    Name = $"{restaurant.Name}-image",
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

    public static async Task SeedMenus(DataContext context)
    {
        var restaurantNumber = await context.Restaurants.CountAsync();
        Random random = new();
        List<string> names = ["Hranu", "Pica", "Deserte", "Pizze", "Dorucak", "Rucak", "Veceru", "Vina"];
        for(int i = 1; i <= restaurantNumber; i++)
        {
            var restaurant = await context.Restaurants.FirstOrDefaultAsync(x => x.Id == i);
            List<Menu> menus = [];
            for (int j = 0; j < 20; j++)
            {
                Menu menu = new()
                {
                    Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit duis volutpat iaculis, dui potenti velit egestas eros libero mus a sem. Ac semper sollicitudin sem litora nascetur rutrum nam venenatis eget facilisi diam proin ut dis, fringilla euismod potenti cubilia bibendum dui neque fermentum augue vulputate luctus eleifend. Lectus molestie sodales nisi cum senectus porta laoreet urna elementum, primis litora at proin vehicula duis feugiat euismod, hendrerit vitae massa suspendisse penatibus class cursus volutpat",
                    IsActive = true,
                    IsDeleted = false,
                    Name = $"Menu za {names[random.Next(8)]} - {j + 1}",
                    RestaurantId = restaurant.Id,
                    Restaurant = restaurant
                };
                menus.Add(menu);
            }

            context.Menus.AddRange(menus);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedMenuItems(DataContext context)
    {
        var menusNumber = await context.Menus.CountAsync();
        Random random = new();
        List<string> names = ["hrana", "sendvice", "pizza", "vino", "pivo", "sok", "kafa", "Caj"];
        for (int i = 1; i <= menusNumber; i++)
        {
            var menu = await context.Menus.FirstOrDefaultAsync(x => x.Id == i);
            List<MenuItem> menuItems = [];
            for (int j = 1; j <= 20; j++)
            {
                MenuItem menuItem = new()
                {
                    Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit conubia magnis, ridiculus aptent lectus semper platea curabitur himenaeos venenatis, tincidunt tempor felis lobortis eu quis tempus turpis. Viverra rutrum facilisis placerat felis neque massa litora et sodales iaculis, justo diam eros libero proin sollicitudin sociis penatibus",
                    HasSpecialOffer = random.Next(2) == 0,
                    IsActive = true,
                    IsDeleted = false,
                    MenuId = menu.Id,
                    Menu = menu,
                    Name = $"{names[random.Next(8)]} - {j}",
                    OrderCount = random.Next(1, 999),
                    Price = random.NextDouble() * (100 - 5) + 5,
                    SpecialOfferPrice = random.NextDouble() * (50 - 2) + 2
                };
                menuItems.Add(menuItem);
            }

            context.MenuItems.AddRange(menuItems);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedMenuItemsImages(DataContext context)
    {
        Random random = new();
        for (int i = 12401; i <= 13400; i++)
        {
            var menuItem = await context.MenuItems.FirstOrDefaultAsync(x => x.Id == i);
            MenuItemImage menuItemImage = new()
            {
                ContentType = "Image",
                IsDeleted = false,
                MenuItemId = menuItem.Id,
                MenuItem = menuItem,
                Name = $"{menuItem.Name}-image{i}",
                Size = random.Next(70000, 100000),
                Type = MenuItemImageType.Profile,
                UniqueName = $"{Guid.NewGuid()}-{menuItem.Name}",
                Url = $"https://picsum.photos/{random.Next(500, 900)}/{random.Next(500, 900)}"
            };
            context.MenuItemImages.Add(menuItemImage);
        }
        await context.SaveChangesAsync();
    }

    public static async Task SeedEmployees(DataContext context, UserManager<AppUser> userManager)
    {
        var restaurantNumber = await context.Restaurants.CountAsync();
        var country = await context.Countries.FindAsync(28);
        Random random = new();
        List<string> cities = ["Doboj", "Tuzla", "Sarajevo", "Zenica", "Mostar", "Banja Luka", "Modrica", "Bjeljina", "Visoko"];
        List<string> names = ["Pero", "Branko", "James", "Jones", "Osman", "Mehmed", "Dalibor", "Milos", "Nermin", "Ermin"];
        List<string> lastNames = ["Jurisic", "Niksic", "Mehmedovic", "Zunic", "Osmancic", "Peric", "LeBron"];
        for (int i = 1; i <= restaurantNumber; i++)
        {
            var restaurant = await context.Restaurants.FindAsync(i);
            List<Employee> employees = [];
            int randomEmployeesNumber = random.Next(3, 8);
            for (int j = 1; j <= randomEmployeesNumber; j++)
            {
                AppUser user = new()
                {
                    UserName = $"{restaurant.Name}-user{j}",
                    Email = $"user-{j}@{restaurant.Name}.com",
                    PhoneNumber = "06032678998"
                };
                await userManager.CreateAsync(user, "LeaveMeAlone21?");

                Employee employee = new()
                {
                    Address = $"Ulica {restaurant.Name} {j}.{j}.",
                    AppUserId = user.Id,
                    AppUser = user,
                    Birth = DateTime.UtcNow,
                    CanEditFolders = random.Next(2) == 0,
                    CanEditMenus = random.Next(2) == 0,
                    CanViewFolders = random.Next(2) == 0,
                    City = $"{cities[random.Next(9)]}",
                    Country = country,
                    CountryId = country.Id,
                    Description = "Lorem ipsum dolor sit amet consectetur adipiscing elit enim tempus, varius habitasse sodales condimentum duis odio quisque iaculis platea vehicula, in dapibus accumsan sociis scelerisque rutrum fringilla torquent. Mus quisque magnis libero eros quam platea curabitur, tellus in eget habitasse lobortis parturient, tempus aliquam proin sociosqu blandit aliquet. Dapibus proin nascetur sociosqu ultricies diam nunc sagittis mauris habitasse",
                    FirstName = $"{names[random.Next(10)]}",
                    IsDeleted = false,
                    LastName = $"{lastNames[random.Next(7)]}",
                    RestaurantId = restaurant.Id,
                    Restaurant = restaurant,
                    UniqueUsername = user.UserName
                };

                employees.Add(employee);
            }

            context.Employees.AddRange(employees);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedUsersImages(DataContext context)
    {
        int userNumber = await context.Users.CountAsync();
        Random random = new();
        for (int i = 1; i <= userNumber; i++)
        {
            var user = await context.Users.FindAsync(i);
            int randomImageNumber = random.Next(3, 6);
            List<AppUserImage> images = [];
            for (int j = 1; j <= randomImageNumber; j++)
            {
                AppUserImage appUserImage = new()
                {
                    AppUserId = user.Id,
                    AppUser = user,
                    ContentType = "Type/Images",
                    IsDeleted = false,
                    Name = "image name",
                    Size = random.Next(70000, 100000),
                    UniqueName = $"{Guid.NewGuid()}",
                    Type = j == 1 ? AppUserImageType.Profile : AppUserImageType.Gallery,
                    Url = $"https://picsum.photos/{random.Next(500, 900)}/{random.Next(500, 900)}"
                };
                images.Add(appUserImage);
            }

            context.AppUserImages.AddRange(images);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedTables(DataContext context)
    {
        var restaurantNumber = await context.Restaurants.CountAsync();
        Random random = new();
        for (int i = 1; i <= restaurantNumber; i++) 
        {
            var tablesNumber = random.Next(10, 31);
            var restaurant = await context.Restaurants.FindAsync(i);
            List<Table> tables = [];
            for (int j = 1; j <= tablesNumber; j++)
            {
                Table table = new()
                {
                    Name = $"Table-{j}",
                    RestaurantId = restaurant.Id,
                    Restaurant = restaurant
                };
                tables.Add(table);
            }

            context.Tables.AddRange(tables);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedCustomers(DataContext context, UserManager<AppUser> userManager)
    {
        for (int i = 1; i <= 20; i++)
        {
            AppUser user = new()
            {
                UserName = $"lastuser{i}"
            };
            await userManager.CreateAsync(user, "NoshNexus21?");
            Customer customer = new()
            {
                AppUserId = user.Id,
                AppUser = user,
                UniqueUsername = user.UserName
            };

            context.Customers.Add(customer);
        }

        await context.SaveChangesAsync();
    }


    public static async Task SeedCountries(DataContext context)
    {
        if (await context.Countries.AnyAsync()) return;

        var countryData = await File.ReadAllTextAsync("Infrastructure/Data/countries.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var countries = JsonSerializer.Deserialize<List<OwnerDtos.CreateCountryDto>>(countryData, options);

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
        var currencies = JsonSerializer.Deserialize<List<OwnerDtos.CreateCountryDto>>(currencyData, options);

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

internal class Appuser
{
}