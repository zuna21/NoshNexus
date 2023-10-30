

namespace API;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IOwnerService _ownerService;
    private readonly ICountryService _countryService;
    public RestaurantService(
        IRestaurantRepository restaurantRepository,
        IOwnerService ownerService,
        ICountryService countryService
    )
    {
        _restaurantRepository = restaurantRepository;
        _ownerService = ownerService;
        _countryService = countryService;
    }
    public async Task<Response<string>> Create(CreateRestaurantDto createRestaurantDto)
    {
        Response<string> response = new();
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
            response.Data = "Successfully created restaurant";
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }
        
        return response;
    }

    public async Task<Response<ICollection<RestaurantCardDto>>> GetOwnerRestaurants()
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

            var restaurants = await _restaurantRepository.GetOwnerRestaurants(owner);
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

    public async Task<Response<RestaurantDetailsDto>> GetRestaurantDetails(int restaurantId)
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
            
            var restaurantDetails = await _restaurantRepository.GetRestaurantDetails(restaurantId, owner);
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

            var getRestaurantEdit = await _restaurantRepository.GetRestaurantEdit(restaurantId, owner);
            if (getRestaurantEdit == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

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

            var restaurantSelect = await _restaurantRepository.GetRestaurantSelect(owner);
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
}
