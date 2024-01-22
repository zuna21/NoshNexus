using ApplicationCore;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace API;

public class SettingRepository(
    DataContext dataContext
) : ISettingRepository
{
    private readonly DataContext _context = dataContext;

    public async Task<RestaurantBlockedCustomers> GetOwnerBlockedCustomer(int ownerId, int customerId)
    {
        return await _context.RestaurantBlockedCustomers
            .FirstOrDefaultAsync(x => x.Restaurant.OwnerId == ownerId && x.CustomerId == customerId);
    }

    public async Task<PagedList<OwnerDtos.CustomerCardDto>> GetOwnerBlockedCustomers(int ownerId, OwnerQueryParams.BlockedCustomersQueryParams blockedCustomersQueryParams)
    {
        var query = _context.RestaurantBlockedCustomers
            .Where(x => x.Restaurant.OwnerId == ownerId);

        if (blockedCustomersQueryParams.Restaurant != -1)
            query = query.Where(x => x.RestaurantId == blockedCustomersQueryParams.Restaurant);

        if (!string.IsNullOrEmpty(blockedCustomersQueryParams.Search))
            query = query.Where(x => x.Customer.UniqueUsername.ToLower().Contains(blockedCustomersQueryParams.Search.ToLower()));

        var totalItems = await query.CountAsync();

        var result = await query
            .Skip(blockedCustomersQueryParams.PageSize * blockedCustomersQueryParams.PageIndex)
            .Take(blockedCustomersQueryParams.PageSize)
            .Select(x => new OwnerDtos.CustomerCardDto
            {
                Id = x.CustomerId,
                FirstName = "Nosh",
                LastName = "Nexus",
                Username = x.Customer.UniqueUsername,
                ProfileImage = x.Customer.AppUser.AppUserImages
                    .Where(ui => ui.IsDeleted == false && ui.Type == AppUserImageType.Profile)
                    .Select(ui => ui.Url)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return new PagedList<OwnerDtos.CustomerCardDto>()
        {
            Result = result,
            TotalItems = totalItems
        };
    }

    public async Task<bool> IsCustomerBlocked(int customerId, int restaurantId)
    {
        return await _context.RestaurantBlockedCustomers
            .AnyAsync(x => x.CustomerId == customerId && x.RestaurantId == restaurantId);
    }

    public void RemoveOwnerBlockedCustomer(RestaurantBlockedCustomers restaurantBlockedCustomers)
    {
        _context.RestaurantBlockedCustomers.Remove(restaurantBlockedCustomers);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
