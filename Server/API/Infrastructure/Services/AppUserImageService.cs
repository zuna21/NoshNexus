
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public class AppUserImageService : IAppUserImageService
{
    private readonly IAppUserImageRepository _appUserImageRepository;
    private readonly IUserService _userService;
    private readonly IHostEnvironment _env;
    private readonly IAppUserRepository _appUserRepository;
    public AppUserImageService(
        IAppUserImageRepository appUserImageRepository,
        IUserService userService,
        IHostEnvironment hostEnvironment,
        IAppUserRepository appUserRepository
    )
    {
        _appUserImageRepository = appUserImageRepository;
        _userService = userService;
        _env = hostEnvironment;
        _appUserRepository = appUserRepository;
    }

    public async Task<ImageDto> GetUserProfileImage()
    {
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                return null;
            }

            var profileImage = await _appUserImageRepository.GetUserProfileImage(user.Id);
            return profileImage;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return null;
    }

    public async Task<ImageDto> UploadProfileImage(int userId, IFormFile image)
    {
        if (image == null)
        {
            return null;
        }

        var fileType = Path.GetExtension(image.FileName);
        if (fileType.ToLower() != ".jpg" && fileType.ToLower() != ".png" && fileType.ToLower() != ".jpeg")
        {
            return null;
        }

        var user = await _appUserRepository.GetUserById(userId);
        if (user == null)
        {
            return null;
        }


        var currentPath = _env.ContentRootPath;  // Ovo je ...../Server/API/
        string directoryName = DateTime.Now.ToString("dd-MM-yyyy");
        var fullPath = Path.Combine(currentPath, "wwwroot", "images", "user", directoryName);
        var relativePath = Path.Combine("images", "user", directoryName);

        Directory.CreateDirectory(fullPath);
        var appUserImage = new AppUserImage
        {
            Name = image.FileName,
            ContentType = image.ContentType,
            Size = image.Length,
            UniqueName = $"{Guid.NewGuid()}-{image.FileName}",
            Type = AppUserImageType.Profile,
            AppUser = user,
            AppUserId = user.Id
        };
        appUserImage.RelativePath = Path.Combine(relativePath, appUserImage.UniqueName);
        appUserImage.FullPath = Path.Combine(fullPath, appUserImage.UniqueName);
        appUserImage.Url = Path.Combine("http://localhost:5000", appUserImage.RelativePath);

        _appUserImageRepository.AddImage(appUserImage);
        using var stream = new FileStream(Path.Combine(fullPath, appUserImage.UniqueName), FileMode.Create);
        await image.CopyToAsync(stream);

        var oldProfileImageEntity = await _appUserImageRepository.GetProfileImage(user.Id);
        if (oldProfileImageEntity != null)
        {
            oldProfileImageEntity.Type = AppUserImageType.Gallery;
        }

        if (!await _appUserImageRepository.SaveAllAsync())
        {
            return null;
        }

        return new ImageDto
        {
            Id = appUserImage.Id,
            Size = appUserImage.Size,
            Url = appUserImage.Url
        };
    }
}
