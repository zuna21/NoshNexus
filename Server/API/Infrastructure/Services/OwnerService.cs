
using Microsoft.AspNetCore.Identity;

namespace API;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly UserManager<IdentityUser> _userManager;
    public OwnerService(
        IOwnerRepository ownerRepository,
        UserManager<IdentityUser> userManager
    )
    {
        _ownerRepository = ownerRepository;
        _userManager = userManager;
    }
    public async Task<Response<OwnerAccountDto>> Register(RegisterOwnerDto registerOwnerDto)
    {
        Response<OwnerAccountDto> response = new();
        try
        {
            var userExists = await _userManager.FindByNameAsync(registerOwnerDto.Username.ToLower());
            if (userExists != null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Username is taken.";
                return response;
            }

            var user = new IdentityUser
            {
                UserName = registerOwnerDto.Username.ToLower(),
                Email = registerOwnerDto.Email,
                PhoneNumber = registerOwnerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerOwnerDto.Password);
            if (!result.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create user.";
                return response;
            }

            // Odavdje nastavitit
            
        }
    }
}
