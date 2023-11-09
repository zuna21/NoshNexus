


namespace API;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IOwnerService _ownerService;
    private readonly ICountryService _countryService;
    private readonly ICurrencyService _currencyService;
    private readonly IHostEnvironment _env;
    private readonly IRestaurantImageRepository _restaurantImageRepository;
    public RestaurantService(
        IRestaurantRepository restaurantRepository,
        IOwnerService ownerService,
        ICountryService countryService,
        ICurrencyService currencyService,
        IHostEnvironment environment,
        IRestaurantImageRepository restaurantImageRepository
    )
    {
        _restaurantRepository = restaurantRepository;
        _ownerService = ownerService;
        _countryService = countryService;
        _currencyService = currencyService;
        _env = environment;
        _restaurantImageRepository = restaurantImageRepository;
    }
    public async Task<Response<int>> Create(CreateRestaurantDto createRestaurantDto)
    {
        Response<int> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "You can't create restaurant.";
                return response;
            }

            var country = await _countryService.GetCountryById(createRestaurantDto.CountryId);
            if (country == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to load country.";
                return response;
            }

            var currency = await _currencyService.GetCurrencyById(createRestaurantDto.CurrencyId);
            if (currency == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to load currency.";
                return response;
            }

            var restaurant = new Restaurant
            {
                OwnerId = owner.Id,
                Owner = owner,
                Address = createRestaurantDto.Address,
                City = createRestaurantDto.City,
                Description = createRestaurantDto.Description,
                FacebookUrl = createRestaurantDto.FacebookUrl,
                InstagramUrl = createRestaurantDto.InstagramUrl,
                CountryId = createRestaurantDto.CountryId,
                Country = country,
                CurrencyId = currency.Id,
                Currency = currency,
                IsActive = createRestaurantDto.IsActive,
                Name = createRestaurantDto.Name,
                PhoneNumber = createRestaurantDto.PhoneNumber,
                PostalCode = createRestaurantDto.PostalCode,
                WebsiteUrl = createRestaurantDto.WebsiteUrl
            };

            _restaurantRepository.Create(restaurant);
            if (!await _restaurantRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create restaurant.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Message = "Successfully Created Restaurant";
            response.Data = restaurant.Id;
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }
        
        return response;
    }

    public async Task<Restaurant> GetOwnerRestaurant(int restaurantId)
    {
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null) return null;
            return await _restaurantRepository.GetOwnerRestaurant(restaurantId, owner.Id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
    }

    public async Task<Response<ICollection<RestaurantCardDto>>> GetRestaurants()
    {
        Response<ICollection<RestaurantCardDto>> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null) 
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "You have no permission to view restaurants";
                return response;
            }

            var restaurants = await _restaurantRepository.GetRestaurants(owner.Id);
            response.Status = ResponseStatus.Success;
            response.Data = restaurants;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<RestaurantDetailsDto>> GetRestaurant(int restaurantId)
    {
        Response<RestaurantDetailsDto> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            
            var restaurantDetails = await _restaurantRepository.GetRestaurant(restaurantId, owner.Id);
            if (restaurantDetails == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = restaurantDetails;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<GetRestaurantEditDto>> GetRestaurantEdit(int restaurantId)
    {
        Response<GetRestaurantEditDto> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var getRestaurantEdit = await _restaurantRepository.GetRestaurantEdit(restaurantId, owner.Id);
            if (getRestaurantEdit == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var allCountries = await _countryService.GetAllCountries();
            var allCurrencies = await _currencyService.GetAllCurrencies();

            getRestaurantEdit.AllCountries = allCountries;
            getRestaurantEdit.AllCurrencies = allCurrencies;


            response.Status = ResponseStatus.Success;
            response.Data = getRestaurantEdit;
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<ICollection<RestaurantSelectDto>>> GetRestaurantSelect()
    {
        Response<ICollection<RestaurantSelectDto>> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var restaurantSelect = await _restaurantRepository.GetRestaurantSelect(owner.Id);
            response.Status = ResponseStatus.Success;
            response.Data = restaurantSelect;
        }

        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }


    public async Task<ICollection<RestaurantSelectDto>> GetRestaurantsForSelect()
    {
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null) return null;
            return await _restaurantRepository.GetRestaurantSelect(owner.Id);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return null;
    }

    public async Task<Response<GetCreateRestaurantDto>> GetCreateRestaurant()
    {
        Response<GetCreateRestaurantDto> response = new();
        try
        {
            var countries = await _countryService.GetAllCountries();
            var currencies = await _currencyService.GetAllCurrencies();
            var createRestaurant = new GetCreateRestaurantDto
            {
                Countries = countries,
                Currencies = currencies
            };

            response.Status = ResponseStatus.Success;
            response.Data = createRestaurant;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<bool>> UploadImages(int restaurantId, IFormFileCollection images)
    {
        Response<bool> response = new();
        try
        {
            bool areAllImages = AreAllImages(images);
            if (!areAllImages)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "You can upload only images.";
                return response;
            }

            var restaurant = await GetOwnerRestaurant(restaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }


            var currentPath = _env.ContentRootPath;  // Ovo je ...../Server/API/
            string directoryName = DateTime.Now.ToString("dd-MM-yyyy");
            var fullPath = Path.Combine(currentPath, "wwwroot", "images", "restaurant", directoryName);
            var relativePath = Path.Combine("images", "restaurant", directoryName);
            if (images == null || images.Count <= 0) 
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "You need to upload at least one image.";
                return response;
            }


            Directory.CreateDirectory(fullPath);
            for (var i=0; i < images.Count; i++)
            {
                var image = images[i];
                var type = RestaurantImageType.Gallery;
                type = image.Name switch
                {
                    "gallery" => RestaurantImageType.Gallery,
                    "profile" => RestaurantImageType.Profile,
                    _ => RestaurantImageType.Gallery,
                };
                var restaurantImage = new RestaurantImage
                {
                    Name = image.FileName,
                    ContentType = image.ContentType,
                    FullPath = fullPath,
                    RelativePath = relativePath,
                    Size = image.Length,
                    UniqueName = $"{Guid.NewGuid()}-{image.FileName}",
                    Type = type,
                    Restaurant = restaurant,
                    RestaurantId = restaurant.Id
                };
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
