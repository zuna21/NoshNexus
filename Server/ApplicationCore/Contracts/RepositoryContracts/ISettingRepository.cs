namespace ApplicationCore;

public interface ISettingRepository
{
    Task<PagedList<CustomerCardDto>> GetOwnerBlockedCustomers(int ownerId, BlockedCustomersQueryParams blockedCustomersQueryParams);
    Task<RestaurantBlockedCustomers> GetOwnerBlockedCustomer(int ownerId, int customerId);
    void RemoveOwnerBlockedCustomer(RestaurantBlockedCustomers restaurantBlockedCustomers);
    Task<bool> SaveAllAsync();
}
