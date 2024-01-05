using ApplicationCore;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class SettingRepository(
    DataContext dataContext
) : ISettingRepository
{
    private readonly DataContext _context = dataContext;
    public async Task<PagedList<CustomerCardDto>> GetOwnerBlockedCustomers(int ownerId, BlockedCustomersQueryParams blockedCustomersQueryParams)
    {
        var query = _context.RestaurantBlockedCustomers
            .Where(x => x.Restaurant.OwnerId == ownerId);

        if (blockedCustomersQueryParams.Restaurant != -1)
            query = query.Where(x => x.RestaurantId == blockedCustomersQueryParams.Restaurant);

        var totalItems = await query.CountAsync();
        
        var result = await query
            .Skip(blockedCustomersQueryParams.PageSize * blockedCustomersQueryParams.PageIndex)
            .Take(blockedCustomersQueryParams.PageSize)
            .Select(x => new CustomerCardDto
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

        return new PagedList<CustomerCardDto>()
        {
            Result = result,
            TotalItems = totalItems
        };
    }
}
