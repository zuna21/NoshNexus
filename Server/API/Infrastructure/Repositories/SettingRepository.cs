using ApplicationCore;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class SettingRepository(
    DataContext dataContext
) : ISettingRepository
{
    private readonly DataContext _context = dataContext;
    public async Task<ICollection<CustomerCardDto>> GetOwnerBlockedCustomers(int ownerId)
    {
        return await _context.RestaurantBlockedCustomers
            .Where(x => x.Restaurant.OwnerId == ownerId)
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
    }
}
