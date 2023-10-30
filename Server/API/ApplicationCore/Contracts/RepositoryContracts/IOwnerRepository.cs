namespace API;

public interface IOwnerRepository
{
    void Create(Owner owner);
    Task<bool> DoesOwnerExists(string username);
    Task<Owner> GetOwnerByUsername(string username);
    Task<bool> SaveAllAsync();
}
