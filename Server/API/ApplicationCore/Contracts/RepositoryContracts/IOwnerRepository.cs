namespace API;

public interface IOwnerRepository
{
    void Create(Owner owner);
    Task<bool> SaveAllAsync();
}
