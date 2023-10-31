namespace API;

public interface IOwnerService
{
    Task<Response<OwnerAccountDto>> Register(RegisterOwnerDto registerOwnerDto);
    Task<Response<OwnerAccountDto>> Login(LoginOwnerDto loginOwnerDto);



    // global functions (not for using in controllers)
    Task<Owner> GetOwner();
}
