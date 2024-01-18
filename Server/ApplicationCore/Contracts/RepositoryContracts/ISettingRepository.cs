namespace ApplicationCore;

using OwnerDtos = DTOs.OwnerDtos;

public interface ISettingRepository
{
    Task<PagedList<OwnerDtos.CustomerCardDto>> GetOwnerBlockedCustomers(int ownerId, BlockedCustomersQueryParams blockedCustomersQueryParams);
    Task<RestaurantBlockedCustomers> GetOwnerBlockedCustomer(int ownerId, int customerId);
    void RemoveOwnerBlockedCustomer(RestaurantBlockedCustomers restaurantBlockedCustomers);
    Task<bool> SaveAllAsync();
}
