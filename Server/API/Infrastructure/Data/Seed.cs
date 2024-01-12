using System.Text.Json;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class Seed
{
    /* public static async Task SeedOrders(DataContext context)
    {
        Random random = new();
        List<Order> orders = [];
        var restaurant = await context.Restaurants.FindAsync(1);
        for (int i = 0; i < 1000; i++)
        {
            var table = await context.Tables
                .Where(x => x.RestaurantId == 1)
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync();
            var customer = await context.Customers
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync();
            var totalItems = random.Next(1, 5);
            List<MenuItem> menuItems = [];
            double totalPrice = 0;
            for (int j = 0; j < totalItems; j++)
            {
                var menuItem = await context.MenuItems
                    .Where(x => x.Menu.RestaurantId == 1)
                    .OrderBy(x => Guid.NewGuid())
                    .FirstOrDefaultAsync();
                totalPrice += menuItem.HasSpecialOffer ? menuItem.SpecialOfferPrice : menuItem.Price;
                menuItems.Add(menuItem);
            }

            Order order = new()
            {
                CustomerId = customer.Id,
                Customer = customer,
                RestaurantId = restaurant.Id,
                Restaurant = restaurant,
                Note = "This is some note inserted.",
                TableId = table.Id,
                Table = table,
                Status = random.Next(1, 3) == 1 ? OrderStatus.Declined : OrderStatus.Accepted,
                TotalPrice = totalPrice,
                TotalItems = totalItems,
                CreatedAt = new DateTime(2023, 12, random.Next(1, 32), 0, 0, 0, DateTimeKind.Utc)
            };
            order.DeclineReason = order.Status == OrderStatus.Accepted ? null : "This is declined reason";
            context.Orders.Add(order);

            List<OrderMenuItem> orderMenuItems = menuItems.Select(x => new OrderMenuItem
            {
                MenuItemId = x.Id,
                OrderId = order.Id,
                MenuItem = x,
                Order = order
            }).ToList();

            context.OrderMenuItems.AddRange(orderMenuItems);

        }

        await context.SaveChangesAsync();
    } */


    /* public static async Task SeedMenuItems(DataContext context)
    {
        Random random = new();
        List<MenuItem> menuItems = [];
        for (int i = 0; i < 500; i++)
        {
            var menu = await context.Menus
                .Where(x => x.RestaurantId == 1)
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync();
            MenuItem menuItem = new()
            {
                Description = $"This is description for menu item {i}",
                IsActive = true,
                IsDeleted = false,
                HasSpecialOffer = random.Next(0, 2) == 0,
                Price = random.NextDouble() * 100,
                MenuId = menu.Id,
                Menu = menu,
                Name = $"Menu Item {i}",
                OrderCount = random.Next(5, 1500),
            };
            menuItem.SpecialOfferPrice = menuItem.HasSpecialOffer ? random.NextDouble() * 100 : 0;
            menuItems.Add(menuItem);
        }

        context.MenuItems.AddRange(menuItems);
        await context.SaveChangesAsync();
    } */


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
