
using ApplicationCore.DTOs;

namespace API;

public class AppUserImageService : IAppUserImageService
{
    private readonly IAppUserImageRepository _appUserImageRepository;
    private readonly IUserService _userService;
    private readonly IHostEnvironment _env;
    public AppUserImageService(
        IAppUserImageRepository appUserImageRepository,
        IUserService userService,
        IHostEnvironment hostEnvironment
    )
    {
        _appUserImageRepository = appUserImageRepository;
        _userService = userService;
        _env = hostEnvironment;
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
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return null;
    }

    public async Task<Response<ImageDto>> UploadProfileImage(IFormFile image)
    {
        Response<ImageDto> response = new();
        try
        {
            if (image == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload image.";
                return response;
            }

            var fileType = Path.GetExtension(image.FileName);
            if (fileType.ToLower() != ".jpg" && fileType.ToLower() != ".png" && fileType.ToLower() != ".jpeg")
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload only image.";
                return response;
            }

            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
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
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to save profile images.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new ImageDto
            {
                Id = appUserImage.Id,
                Size = appUserImage.Size,
                Url = appUserImage.Url
            };
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
