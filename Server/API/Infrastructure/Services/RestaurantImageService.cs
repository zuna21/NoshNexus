

using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public class RestaurantImageService : IRestaurantImageService
{
    private readonly IRestaurantImageRepository _restaurantImageRepository;
    private readonly IRestaurantService _restaurantService;
    private readonly IHostEnvironment _env;
    public RestaurantImageService(
        IRestaurantImageRepository restaurantImageRepository,
        IRestaurantService restaurantService,
        IHostEnvironment hostEnvironment
    )
    {
        _restaurantImageRepository = restaurantImageRepository;
        _restaurantService = restaurantService;
        _env = hostEnvironment;
    }

    public async Task<Response<bool>> Delete(int restaurantId, int imageId)
    {
        Response<bool> response = new();
        try
        {
            var image = await _restaurantImageRepository.GetImage(restaurantId, imageId);
            if (image == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            image.IsDeleted = true;
            if (!await _restaurantImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete image";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = true;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
        }

        return response;
    }

    public async Task<Response<ICollection<ImageDto>>> UploadImages(int restaurantId, IFormFileCollection images)
    {
        Response<ICollection<ImageDto>> response = new();
        try
        {
            if (images == null || images.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Upload at least one image.";
                return response;
            }

            var areAllImagesVar = AreAllImages(images);
            if(!areAllImagesVar)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload only image.";
                return response;
            }

            var restaurant = await _restaurantService.GetOwnerRestaurant(restaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }


            var currentPath = _env.ContentRootPath;  // Ovo je ...../Server/API/
            string directoryName = DateTime.Now.ToString("dd-MM-yyyy");
            var fullPath = Path.Combine(currentPath, "wwwroot", "images", "restaurant", directoryName);
            var relativePath = Path.Combine("images", "restaurant", directoryName);

            Directory.CreateDirectory(fullPath);
            foreach (var image in images)
            {
                var restaurantImage = new RestaurantImage
                {
                    Name = image.FileName,
                    ContentType = image.ContentType,
                    Size = image.Length,
                    UniqueName = $"{Guid.NewGuid()}-{image.FileName}",
                    Type = RestaurantImageType.Gallery,
                    Restaurant = restaurant,
                    RestaurantId = restaurant.Id
                };
                restaurantImage.RelativePath = Path.Combine(relativePath, restaurantImage.UniqueName);
                restaurantImage.FullPath = Path.Combine(fullPath, restaurantImage.UniqueName);
                restaurantImage.Url = Path.Combine("http://localhost:5000", restaurantImage.RelativePath);

                _restaurantImageRepository.AddImage(restaurantImage);
                using var stream = new FileStream(Path.Combine(fullPath, restaurantImage.UniqueName), FileMode.Create);
                await image.CopyToAsync(stream);
            }

            if (!await _restaurantImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to save images.";
                return response;
            }

            var galleryImages = await _restaurantImageRepository.GetRestaurantGalleryImages(restaurantId);
            if (galleryImages == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }


            response.Status = ResponseStatus.Success;
            response.Data = galleryImages;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ChangeProfileImageDto>> UploadProfileImage(int restaurantId, IFormFile image)
    {
        Response<ChangeProfileImageDto> response = new();
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
            var restaurant = await _restaurantService.GetOwnerRestaurant(restaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var currentPath = _env.ContentRootPath;  // Ovo je ...../Server/API/
            string directoryName = DateTime.Now.ToString("dd-MM-yyyy");
            var fullPath = Path.Combine(currentPath, "wwwroot", "images", "restaurant", directoryName);
            var relativePath = Path.Combine("images", "restaurant", directoryName);

            Directory.CreateDirectory(fullPath);
            var restaurantImage = new RestaurantImage
            {
                Name = image.FileName,
                ContentType = image.ContentType,
                Size = image.Length,
                UniqueName = $"{Guid.NewGuid()}-{image.FileName}",
                Type = RestaurantImageType.Profile,
                Restaurant = restaurant,
                RestaurantId = restaurant.Id
            };
            restaurantImage.RelativePath = Path.Combine(relativePath, restaurantImage.UniqueName);
            restaurantImage.FullPath = Path.Combine(fullPath, restaurantImage.UniqueName);
            restaurantImage.Url = Path.Combine("http://localhost:5000", restaurantImage.RelativePath);

            _restaurantImageRepository.AddImage(restaurantImage);
            using var stream = new FileStream(Path.Combine(fullPath, restaurantImage.UniqueName), FileMode.Create);
            await image.CopyToAsync(stream);

            var oldProfileImageEntity = await _restaurantImageRepository.GetProfileImage(restaurantId);
            if (oldProfileImageEntity != null)
            {
                oldProfileImageEntity.Type = RestaurantImageType.Gallery;
            }

            if (!await _restaurantImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to save profile images.";
                return response;
            }


            var newProfileImage = new ImageDto
            {
                Id = restaurantImage.Id,
                Size = restaurantImage.Size,
                Url = restaurantImage.Url
            };

            var oldProfileImage = oldProfileImageEntity != null ? new ImageDto
            {
                Id = oldProfileImageEntity.Id,
                Size = oldProfileImageEntity.Size,
                Url = oldProfileImageEntity.Url
            } : null;

            response.Status = ResponseStatus.Success;
            response.Data = new ChangeProfileImageDto
            {
                NewProfileImage = newProfileImage,
                OldProfileImage = oldProfileImage
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


    private bool AreAllImages(IFormFileCollection images)
    {
        for (var i=0; i < images.Count; i++)
        {
            var image = images[i];
            var fileType = Path.GetExtension(image.FileName);
            if (fileType.ToLower() != ".jpg" && fileType.ToLower() != ".png" && fileType.ToLower() != ".jpeg")
            {
                return false;
            }
        }

        return true;
    }
}
