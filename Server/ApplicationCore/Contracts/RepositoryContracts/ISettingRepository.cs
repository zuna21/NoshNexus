namespace ApplicationCore;

public interface ISettingRepository
{
    Task<PagedList<CustomerCardDto>> GetOwnerBlockedCustomers(int ownerId, BlockedCustomersQueryParams blockedCustomersQueryParams);
}
