
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Azure.Storage;
using Azure.Storage.Blobs;

namespace API;

public class MenuItemImageService(
    IMenuItemImageRepository menuItemImageRepository,
    IUserService userService,
    IMenuItemRepository menuItemRepository,
    IConfiguration config
    ) : IMenuItemImageService
{
    private readonly IMenuItemImageRepository _menuItemImageRepository = menuItemImageRepository;
    private readonly IUserService _userService = userService;
    private readonly IMenuItemRepository _menuItemRepository = menuItemRepository;
    private readonly IConfiguration _config = config;

    public async Task<Response<int>> DeleteImage(int imageId)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            var employee = await _userService.GetEmployee();

            var menuItemImage = owner != null ?
                await _menuItemImageRepository.GetOwnerMenuItemImage(imageId, owner.Id) :
                await _menuItemImageRepository.GetRestaurantMenuItemImage(imageId, employee.RestaurantId);

            if(menuItemImage == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            
            menuItemImage.IsDeleted = true;
            if (!await _menuItemImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete image.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menuItemImage.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ImageDto>> UploadProfileImage(int menuItemId, IFormFile image)
    {
        Response<ImageDto> response = new();
        try
        {
            if (!image.ContentType.Contains("image"))
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload only image.";
                return response;
            }

            // korisnik mora biti vlasnik ili employee
            var owner = await _userService.GetOwner();
            var employee = await _userService.GetEmployee();
            if (owner == null && employee == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "You can't upload menu item profile image.";
                return response;
            }

            var menuItem = owner != null ? 
                await _menuItemRepository.GetOwnerMenuItem(menuItemId, owner.Id) :
                await _menuItemRepository.GetRestaurantMenuItemEntity(employee.RestaurantId, menuItemId);
            
            if (menuItem == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var currentDate = DateTime.UtcNow.ToString("dd-MM-yyyy");

            // Azure Storage
            var asAccount = _config["ASAccount"];
            var asKey = _config["ASAccountKey"];
            var creditential = new StorageSharedKeyCredential(asAccount, asKey);
            var accountUrl = $"https://{asAccount}.blob.core.windows.net";
            var blobServiceClient = new BlobServiceClient(new Uri(accountUrl), creditential);

            // container mora biti kreiran u Azure Storage
            var _menuItemContainer = blobServiceClient.GetBlobContainerClient("menu-item-images");
            //

            var uniqueImageName = $"{Guid.NewGuid()}-{image.FileName}";
            BlobClient client = _menuItemContainer.GetBlobClient($"{currentDate}/{uniqueImageName}");
            await using (Stream data = image.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            var oldProfileImage = await _menuItemImageRepository.GetProfileImage(menuItem.Id);
            if (oldProfileImage != null)
            {
                oldProfileImage.IsDeleted = true;
            }

            MenuItemImage menuItemImage = new()
            {
                ContainerName = client.BlobContainerName,
                ContentType = image.ContentType,
                IsDeleted = false,
                MenuItemId = menuItem.Id,
                MenuItem = menuItem,
                Name = image.FileName,
                Size = image.Length,
                Type = MenuItemImageType.Profile,
                UniqueName = uniqueImageName,
                Url = client.Uri.AbsoluteUri
            };

            _menuItemImageRepository.AddImage(menuItemImage);

            if(!await _menuItemImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to upload profile image.";
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
