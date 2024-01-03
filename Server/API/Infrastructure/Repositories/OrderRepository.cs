
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

    public async Task<ICollection<OrderCardDto>> GetCustomerAcceptedOrders(int customerId, string sq)
    {
        return await _context.Orders
            .Where(x => x.CustomerId == customerId && x.Status == OrderStatus.Accepted)
            .Where(x => 
                x.Restaurant.Name.ToLower().Contains(sq.ToLower()) || 
                x.Restaurant.City.ToLower().Contains(sq.ToLower())
            )
            .Select(x => new OrderCardDto
            {
                CreatedAt = x.CreatedAt,
                DeclineReason = x.DeclineReason,
                Id = x.Id,
                Note = x.Note,
                Restaurant = new OrderRestaurantDto
                {
                    Id = x.RestaurantId,
                    Name = x.Restaurant.Name
                },
                Status = x.Status.ToString(),
                TableName = x.Table.Name,
                TotalPrice = x.TotalPrice,
                TotalItems = x.TotalItems,
                User = new OrderCardUserDto
                {
                    Id = x.CustomerId,
                    FirstName = "",
                    LastName = "",
                    ProfileImage = x.Customer.AppUser.AppUserImages
                        .Where(pi => pi.IsDeleted == false && pi.Type == AppUserImageType.Profile)
                        .Select(pi => pi.Url)
                        .FirstOrDefault(),
                    Username = x.Customer.UniqueUsername
                },
                Items = x.OrderMenuItems
                    .Select(omi => new OrderMenuItemDto
                    {
                        Id = omi.MenuItemId,
                        Name = omi.MenuItem.Name,
                        Price = omi.MenuItem.HasSpecialOffer ? omi.MenuItem.SpecialOfferPrice : omi.MenuItem.Price
                    })
                    .ToList()
            })
            .ToListAsync();
    }

    public async Task<ICollection<OrderCardDto>> GetCustomerDeclinedOrders(int customerId, string sq)
    {
        return await _context.Orders
            .Where(x => x.CustomerId == customerId && x.Status == OrderStatus.Declined)
            .Where(x => 
                x.Restaurant.Name.ToLower().Contains(sq.ToLower()) || 
                x.Restaurant.City.ToLower().Contains(sq.ToLower())
            )
            .Select(x => new OrderCardDto
            {
                CreatedAt = x.CreatedAt,
                DeclineReason = x.DeclineReason,
                Id = x.Id,
                Note = x.Note,
                Restaurant = new OrderRestaurantDto
                {
                    Id = x.RestaurantId,
                    Name = x.Restaurant.Name
                },
                Status = x.Status.ToString(),
                TableName = x.Table.Name,
                TotalPrice = x.TotalPrice,
                TotalItems = x.TotalItems,
                User = new OrderCardUserDto
                {
                    Id = x.CustomerId,
                    FirstName = "",
                    LastName = "",
                    ProfileImage = x.Customer.AppUser.AppUserImages
                        .Where(pi => pi.IsDeleted == false && pi.Type == AppUserImageType.Profile)
                        .Select(pi => pi.Url)
                        .FirstOrDefault(),
                    Username = x.Customer.UniqueUsername
                },
                Items = x.OrderMenuItems
                    .Select(omi => new OrderMenuItemDto
                    {
                        Id = omi.MenuItemId,
                        Name = omi.MenuItem.Name,
                        Price = omi.MenuItem.HasSpecialOffer ? omi.MenuItem.SpecialOfferPrice : omi.MenuItem.Price
                    })
                    .ToList()
            })
            .ToListAsync();
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
                        Status = o.Status.ToString(),
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

    public async Task<ICollection<OrderCardDto>> GetCustomerOrders(int customerId, string sq)
    {
        return await _context.Orders
            .Where(x => x.CustomerId == customerId)
            .Where(x => x.Status == OrderStatus.Accepted || x.Status == OrderStatus.Declined)
            .Where(x => 
                x.Restaurant.Name.ToLower().Contains(sq.ToLower()) || 
                x.Restaurant.City.ToLower().Contains(sq.ToLower())
            )
            .Select(x => new OrderCardDto
            {
                CreatedAt = x.CreatedAt,
                DeclineReason = x.DeclineReason,
                Id = x.Id,
                Note = x.Note,
                Restaurant = new OrderRestaurantDto
                {
                    Id = x.RestaurantId,
                    Name = x.Restaurant.Name
                },
                Status = x.Status.ToString(),
                TableName = x.Table.Name,
                TotalPrice = x.TotalPrice,
                TotalItems = x.TotalItems,
                User = new OrderCardUserDto
                {
                    Id = x.CustomerId,
                    FirstName = "",
                    LastName = "",
                    ProfileImage = x.Customer.AppUser.AppUserImages
                        .Where(pi => pi.IsDeleted == false && pi.Type == AppUserImageType.Profile)
                        .Select(pi => pi.Url)
                        .FirstOrDefault(),
                    Username = x.Customer.UniqueUsername
                },
                Items = x.OrderMenuItems
                    .Select(omi => new OrderMenuItemDto
                    {
                        Id = omi.MenuItemId,
                        Name = omi.MenuItem.Name,
                        Price = omi.MenuItem.HasSpecialOffer ? omi.MenuItem.SpecialOfferPrice : omi.MenuItem.Price
                    })
                    .ToList()
            })
            .ToListAsync();
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
                Status = x.Status.ToString(),
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

    public async Task<ICollection<OrderCardDto>> GetOrdersHistory(int ownerId)
    {
        return await _context.Orders
            .Where(x => x.Restaurant.OwnerId == ownerId && x.Status != OrderStatus.InProgress)
            .Select(x => new OrderCardDto
            {
                CreatedAt = x.CreatedAt,
                DeclineReason = x.DeclineReason,
                Id = x.Id,
                Note = x.Note,
                Restaurant = new OrderRestaurantDto
                {
                    Id = x.RestaurantId,
                    Name = x.Restaurant.Name
                },
                Status = x.Status.ToString(),
                TableName = x.Table.Name,
                TotalItems = x.TotalItems,
                TotalPrice = x.TotalPrice,
                User = new OrderCardUserDto
                {
                    Id = x.CustomerId,
                    FirstName = "",
                    LastName = "",
                    ProfileImage = x.Customer.AppUser.AppUserImages
                        .Where(ui => ui.IsDeleted == false && ui.Type == AppUserImageType.Profile)
                        .Select(ui => ui.Url)
                        .FirstOrDefault(),
                    Username = x.Customer.UniqueUsername
                },
                Items = x.OrderMenuItems
                    .Select(omi => new OrderMenuItemDto
                    {
                        Id = omi.MenuItemId,
                        Name = omi.MenuItem.Name,
                        Price = omi.MenuItem.Price
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
                Status = x.Status.ToString(),
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

    public async Task<Order> GetRestaurantOrderById(int orderId, int restaurantId)
    {
        return await _context.Orders
            .Where(x => x.Id == orderId && x.RestaurantId == restaurantId)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}
