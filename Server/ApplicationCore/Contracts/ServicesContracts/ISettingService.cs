using ApplicationCore.DTOs;

namespace ApplicationCore;

public interface ISettingService
{
    Task<Response<ICollection<CustomerCardDto>>> GetOwnerBlockedCustomers();
}
