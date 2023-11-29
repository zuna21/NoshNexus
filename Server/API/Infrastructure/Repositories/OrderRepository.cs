
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
