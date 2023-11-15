
namespace API;

public class MenuItemImageService : IMenuItemImageService
{
    private readonly IMenuItemImageRepository _menuItemImageRepository;
    private readonly IMenuItemService _menuItemService;
    private readonly IHostEnvironment _env;
    public MenuItemImageService(
        IMenuItemImageRepository menuItemImageRepository,
        IMenuItemService menuItemService,
        IHostEnvironment hostEnvironment
    )
    {
        _menuItemImageRepository = menuItemImageRepository;
        _menuItemService = menuItemService;
        _env = hostEnvironment;
    }
    public async Task<Response<ImageDto>> UploadProfileImage(int menuItemId, IFormFile image)
    {
        Response<ImageDto> response = new();
        try
        {
            if (image == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please uplaod image.";
                return response;
            }
            var fileType = Path.GetExtension(image.FileName);
            if (fileType.ToLower() != ".jpg" && fileType.ToLower() != ".png" && fileType.ToLower() != ".jpeg")
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload only image.";
                return response;
            }

            var menuItem = await _menuItemService.GetOwnerMenuItem(menuItemId);
            if (menuItem == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var currentPath = _env.ContentRootPath;  // Ovo je ...../Server/API/
            string directoryName = DateTime.Now.ToString("dd-MM-yyyy");
            var fullPath = Path.Combine(currentPath, "wwwroot", "images", "menuItem", directoryName);
            var relativePath = Path.Combine("images", "menuItem", directoryName);

            Directory.CreateDirectory(fullPath);
            var menuItemImage = new MenuItemImage
            {
                Name = image.FileName,
                ContentType = image.ContentType,
                Size = image.Length,
                UniqueName = $"{Guid.NewGuid()}-{image.FileName}",
                Type = MenuItemImageType.Profile,
                MenuItem = menuItem,
                MenuItemId = menuItem.Id
            };
            menuItemImage.RelativePath = Path.Combine(relativePath, menuItemImage.UniqueName);
            menuItemImage.FullPath = Path.Combine(fullPath, menuItemImage.UniqueName);
            menuItemImage.Url = Path.Combine("http://localhost:5000", menuItemImage.RelativePath);

            _menuItemImageRepository.AddImage(menuItemImage);
            using var stream = new FileStream(Path.Combine(fullPath, menuItemImage.UniqueName), FileMode.Create);
            await image.CopyToAsync(stream);

            var oldProfileImageEntity = await _menuItemImageRepository.GetProfileImage(menuItem.Id);
            if (oldProfileImageEntity != null)
            {
                oldProfileImageEntity.Type = MenuItemImageType.Gallery;
            }

            if (!await _menuItemImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to save profile images.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new ImageDto
            {
                Id = menuItemImage.Id,
                Size = menuItemImage.Size,
                Url = menuItemImage.Url
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
