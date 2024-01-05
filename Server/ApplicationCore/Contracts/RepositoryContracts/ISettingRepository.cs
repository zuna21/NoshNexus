namespace ApplicationCore;

public interface ISettingRepository
{
    Task<ICollection<CustomerCardDto>> GetOwnerBlockedCustomers(int ownerId);
}
