

using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Azure.Storage;
using Azure.Storage.Blobs;

namespace API;

public class RestaurantImageService(
    IRestaurantImageRepository restaurantImageRepository,
    IRestaurantRepository restaurantRepository,
    IUserService userService,
    IConfiguration config
    ) : IRestaurantImageService
{
    private readonly IRestaurantImageRepository _restaurantImageRepository = restaurantImageRepository;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IUserService _userService = userService;
    private readonly IConfiguration _config = config;

    public async Task<Response<bool>> Delete(int restaurantId, int imageId)
    {
        Response<bool> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var restaurant = await _restaurantRepository.GetOwnerRestaurant(restaurantId, owner.Id);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var image = await _restaurantImageRepository.GetImage(restaurant.Id, imageId);
            if (image == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            image.IsDeleted = true;
            if (!await _restaurantImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete image.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = true;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }
        return response;
    }

    public Task<Response<ICollection<ImageDto>>> UploadImages(int restaurantId, IFormFileCollection images)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<ChangeProfileImageDto>> UploadProfileImage(int restaurantId, IFormFile image)
    {
        Response<ChangeProfileImageDto> response = new();
        try
        {
            if (!image.ContentType.Contains("image"))
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload only images.";
                return response;
            }

            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var restaurant = await _restaurantRepository.GetOwnerRestaurant(restaurantId, owner.Id);
            if (restaurant == null)
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
            var _restaurantContainer = blobServiceClient.GetBlobContainerClient("restaurant-images");
            //

            var uniqueImageName = $"{Guid.NewGuid()}-{image.FileName}";
            BlobClient client = _restaurantContainer.GetBlobClient($"{currentDate}/{uniqueImageName}");
            await using (Stream data = image.OpenReadStream())
            {
                await client.UploadAsync(data);
            }


            var oldProfileImage = await _restaurantImageRepository.GetProfileImage(restaurant.Id);
            if (oldProfileImage != null)
            {
                oldProfileImage.Type = RestaurantImageType.Gallery;
            }

            RestaurantImage restaurantImage = new()
            {
                ContainerName = client.BlobContainerName,
                ContentType = image.ContentType,
                IsDeleted = false,
                Name = image.FileName,
                RestaurantId = restaurant.Id,
                Restaurant = restaurant,
                Size = image.Length,
                Type = RestaurantImageType.Profile,
                UniqueName = uniqueImageName,
                Url = client.Uri.AbsoluteUri
            };

            _restaurantImageRepository.AddImage(restaurantImage);
            if (!await _restaurantImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to add profile image.";
                return response;
            }


            ChangeProfileImageDto imageResponse = new()
            {
                NewProfileImage = new ImageDto
                {
                    Id = restaurantImage.Id,
                    Size = restaurantImage.Size,
                    Url = restaurantImage.Url
                },
                OldProfileImage = oldProfileImage != null ? new ImageDto
                {
                    Id = oldProfileImage.Id,
                    Size = oldProfileImage.Size,
                    Url = oldProfileImage.Url
                } : null
            };

            response.Status = ResponseStatus.Success;
            response.Data = imageResponse;
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
