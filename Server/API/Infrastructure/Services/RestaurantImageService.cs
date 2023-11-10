
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
    public async Task<Response<ImageDto>> UploadProfileImage(int restaurantId, IFormFile image)
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
                FullPath = fullPath,
                RelativePath = relativePath,
                Size = image.Length,
                UniqueName = $"{Guid.NewGuid()}-{image.FileName}",
                Type = RestaurantImageType.Profile,
                Restaurant = restaurant,
                RestaurantId = restaurant.Id
            };
            _restaurantImageRepository.AddImage(restaurantImage);
            using var stream = new FileStream(Path.Combine(fullPath, restaurantImage.UniqueName), FileMode.Create);
            await image.CopyToAsync(stream);

            var oldProfileImage = await _restaurantImageRepository.GetProfileImage(restaurantId);
            if (oldProfileImage != null)
            {
                oldProfileImage.Type = RestaurantImageType.Gallery;
            }

            if (!await _restaurantImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to save profile images.";
                return response;
            }


            var profileImage = new ImageDto
            {
                Id = restaurantImage.Id,
                Size = restaurantImage.Size,
                Url = Path.Combine(restaurantImage.RelativePath, restaurantImage.UniqueName)
            };

            response.Status = ResponseStatus.Success;
            response.Data = profileImage;

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
