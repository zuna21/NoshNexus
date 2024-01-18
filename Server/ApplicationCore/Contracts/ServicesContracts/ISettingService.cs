using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace ApplicationCore;

public interface ISettingService
{
    Task<Response<PagedList<OwnerDtos.CustomerCardDto>>> GetOwnerBlockedCustomers(OwnerQueryParams.BlockedCustomersQueryParams blockedCustomersQueryParams);
    Task<Response<int>> UnblockCustomer(int customerId);
}
