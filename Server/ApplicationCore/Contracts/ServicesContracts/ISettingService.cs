using ApplicationCore.DTOs;

namespace ApplicationCore;

public interface ISettingService
{
    Task<Response<PagedList<CustomerCardDto>>> GetOwnerBlockedCustomers(BlockedCustomersQueryParams blockedCustomersQueryParams);
    Task<Response<int>> UnblockCustomer(int customerId);
}
