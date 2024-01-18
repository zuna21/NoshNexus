using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore;

public interface ISettingService
{
    Task<Response<PagedList<OwnerDtos.CustomerCardDto>>> GetOwnerBlockedCustomers(BlockedCustomersQueryParams blockedCustomersQueryParams);
    Task<Response<int>> UnblockCustomer(int customerId);
}
