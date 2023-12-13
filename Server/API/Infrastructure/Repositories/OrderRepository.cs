﻿
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    public OrderRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void Create(Order order)
    {
        _context.Orders.Add(order);
    }

    public void CreateOrderMenuItems(ICollection<OrderMenuItem> orderMenuItems)
    {
        _context.OrderMenuItems.AddRange(orderMenuItems);
    }

    public async Task<CustomerLiveRestaurantOrdersDto> GetCustomerInProgressOrders(int restaurantId)
    {
        return await _context.Restaurants
            .Where(x => x.Id == restaurantId && x.IsDeleted == false)
            .Select(x => new CustomerLiveRestaurantOrdersDto
            {
                Restaurant = new OrderRestaurantDto
                {
                    Id = x.Id,
                    Name = x.Name
                },
                Orders = x.Orders
                    .Where(o => o.Status == OrderStatus.InProgress)
                    .Select(o => new OrderCardDto
                    {
                        CreatedAt = o.CreatedAt,
                        DeclineReason = o.DeclineReason,
                        Id = o.Id,
                        Note = o.Note,
                        Restaurant = new OrderRestaurantDto
                        {
                            Id = o.RestaurantId,
                            Name = o.Restaurant.Name
                        },
                        Status = "inProgress",
                        TableName = o.Table.Name,
                        User = new OrderCardUserDto
                        {
                            Id = o.CustomerId,
                            FirstName = "",
                            LastName = "",
                            ProfileImage = o.Customer.AppUser.AppUserImages
                                .Where(pi => pi.IsDeleted == false && pi.Type == AppUserImageType.Profile)
                                .Select(pi => pi.Url)
                                .FirstOrDefault(),
                            Username = o.Customer.UniqueUsername
                        },
                        TotalItems = o.OrderMenuItems.Count,
                        TotalPrice = o.TotalPrice,
                        Items = o.OrderMenuItems
                            .Select(mi => new OrderMenuItemDto
                            {
                                Id = mi.MenuItemId,
                                Name = mi.MenuItem.Name,
                                Price = mi.MenuItem.HasSpecialOffer ? mi.MenuItem.SpecialOfferPrice : mi.MenuItem.Price
                            })
                            .ToList()
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<OrderCardDto>> GetEmployeeInProgressOrders(int restaurantId)
    {
        return await _context.Orders
            .Where(x => x.RestaurantId == restaurantId && x.Status == OrderStatus.InProgress)
            .Select(x => new OrderCardDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                DeclineReason = x.DeclineReason,
                Note = x.Note,
                TableName = x.Table.Name,
                Status = "inProgress",
                TotalItems = x.TotalItems,
                TotalPrice = x.TotalPrice,
                User = new OrderCardUserDto
                {
                    Id = x.CustomerId,
                    FirstName = "",
                    LastName = "",
                    Username = x.Customer.UniqueUsername,
                    ProfileImage = x.Customer.AppUser.AppUserImages
                        .Where(ui => ui.IsDeleted == false && ui.Type == AppUserImageType.Profile)
                        .Select(ui => ui.Url)
                        .FirstOrDefault()
                },
                Restaurant = new OrderRestaurantDto
                {
                    Id = x.RestaurantId,
                    Name = x.Restaurant.Name
                },
                Items = x.OrderMenuItems
                    .Select(mi => new OrderMenuItemDto
                    {
                        Id = mi.MenuItemId,
                        Name = mi.MenuItem.Name,
                        Price = mi.MenuItem.HasSpecialOffer ? mi.MenuItem.SpecialOfferPrice : mi.MenuItem.Price
                    })
                    .ToList()
            })
            .ToListAsync();
    }

    public async Task<ICollection<OrderCardDto>> GetOwnerInProgressOrders(int ownerId)
    {
        return await _context.Orders
            .Where(x => 
                x.Restaurant.OwnerId == ownerId && 
                x.Status == OrderStatus.InProgress &&
                x.Restaurant.IsDeleted == false
            )
            .Select(x => new OrderCardDto
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                DeclineReason = x.DeclineReason,
                Note = x.Note,
                TableName = x.Table.Name,
                TotalItems = x.TotalItems,
                TotalPrice = x.TotalPrice,
                Status = "inProgress",
                User = new OrderCardUserDto
                {
                    Id = x.CustomerId,
                    FirstName = "",
                    LastName = "",
                    ProfileImage = "",
                    Username = x.Customer.UniqueUsername
                },
                Restaurant = new OrderRestaurantDto
                {
                    Id = x.RestaurantId,
                    Name = x.Restaurant.Name
                },
                Items = x.OrderMenuItems.Select(omi => new OrderMenuItemDto
                {
                    Id = omi.MenuItemId,
                    Name = omi.MenuItem.Name,
                    Price = omi.MenuItem.HasSpecialOffer ? omi.MenuItem.SpecialOfferPrice : omi.MenuItem.Price
                })
                .ToList()
            })
            .ToListAsync();

    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}
