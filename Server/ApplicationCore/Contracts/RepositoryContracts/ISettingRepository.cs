namespace ApplicationCore;

using OwnerDtos = DTOs.OwnerDtos;

using OwnerQueryParams = QueryParams.OwnerQueryParams;

public interface ISettingRepository
{
    Task<PagedList<OwnerDtos.CustomerCardDto>> GetOwnerBlockedCustomers(int ownerId, OwnerQueryParams.BlockedCustomersQueryParams blockedCustomersQueryParams);
    Task<RestaurantBlockedCustomers> GetOwnerBlockedCustomer(int ownerId, int customerId);
    void RemoveOwnerBlockedCustomer(RestaurantBlockedCustomers restaurantBlockedCustomers);
    Task<bool> IsCustomerBlocked(int customerId, int restaurantId);
    Task<bool> SaveAllAsync();
}
