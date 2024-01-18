using ApplicationCore;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace API;

public class SettingService(
    ISettingRepository settingRepository,
    IUserService userService
) : ISettingService
{
    private readonly ISettingRepository _settingRepository = settingRepository;
    private readonly IUserService _userService = userService;
    public async Task<Response<PagedList<OwnerDtos.CustomerCardDto>>> GetOwnerBlockedCustomers(BlockedCustomersQueryParams blockedCustomersQueryParams)
    {
        Response<PagedList<OwnerDtos.CustomerCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _settingRepository.GetOwnerBlockedCustomers(owner.Id, blockedCustomersQueryParams);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<int>> UnblockCustomer(int customerId)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var ownerBlockedCustomer = await _settingRepository.GetOwnerBlockedCustomer(owner.Id, customerId);
            if (ownerBlockedCustomer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            _settingRepository.RemoveOwnerBlockedCustomer(ownerBlockedCustomer);
            if (!await _settingRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to unblock user.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = ownerBlockedCustomer.CustomerId;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }
}
