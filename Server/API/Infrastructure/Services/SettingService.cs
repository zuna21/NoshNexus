using ApplicationCore;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;

namespace API;

public class SettingService(
    ISettingRepository settingRepository,
    IUserService userService
) : ISettingService
{
    private readonly ISettingRepository _settingRepository = settingRepository;
    private readonly IUserService _userService = userService;
    public async Task<Response<ICollection<CustomerCardDto>>> GetOwnerBlockedCustomers()
    {
        Response<ICollection<CustomerCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _settingRepository.GetOwnerBlockedCustomers(owner.Id);
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
