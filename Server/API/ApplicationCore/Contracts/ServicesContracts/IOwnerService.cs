namespace API;

public interface IOwnerService
{
    Task<Response<OwnerAccountDto>> Register(RegisterOwnerDto registerOwnerDto);
}
