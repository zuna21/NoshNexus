namespace API;

public interface IOwnerRepository
{
    void Create(Owner owner);
    Task<bool> DoesOwnerExists(string username);
    Task<bool> SaveAllAsync();
}
