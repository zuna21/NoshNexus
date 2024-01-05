


using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public class RestaurantService(
    IRestaurantRepository restaurantRepository,
    IUserService userService,
    ICountryRepository countryRepository,
    ICurrencyRepository currencyRepository
    ) : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IUserService _userService = userService;
    private readonly ICountryRepository _countryRepository = countryRepository;
    private readonly ICurrencyRepository _currencyRepository = currencyRepository;

    public async Task<Response<int>> Create(CreateRestaurantDto createRestaurantDto)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "You can't create restaurant.";
                return response;
            }

            var country = await _countryRepository.GetCountryById(createRestaurantDto.CountryId);
            if (country == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to load country.";
                return response;
            }

            var currency = await _currencyRepository.GetCurrencyById(createRestaurantDto.CurrencyId);
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

    public async Task<Response<ICollection<RestaurantCardDto>>> GetRestaurants(RestaurantsQueryParams restaurantsQueryParams)
    {
        Response<ICollection<RestaurantCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null) 
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "You have no permission to view restaurants";
                return response;
            }

            var restaurants = await _restaurantRepository.GetRestaurants(owner.Id, restaurantsQueryParams);
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
            var owner = await _userService.GetOwner();
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
            var owner = await _userService.GetOwner();
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

            var allCountries = await _countryRepository.GetAllCountries();
            var allCurrencies = await _currencyRepository.GetAllCurrencies();

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
            var owner = await _userService.GetOwner();
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

    public async Task<Response<GetCreateRestaurantDto>> GetCreateRestaurant()
    {
        Response<GetCreateRestaurantDto> response = new();
        try
        {
            var countries = await _countryRepository.GetAllCountries();
            var currencies = await _currencyRepository.GetAllCurrencies();
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

    public async Task<Response<bool>> Update(int restaurantId, RestaurantEditDto restaurantEditDto)
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

            if (restaurant.CurrencyId != restaurantEditDto.CurrencyId)
            {
                var currency = await _currencyRepository.GetCurrencyById(restaurantEditDto.CurrencyId);
                if (currency == null)
                {
                    response.Status = ResponseStatus.NotFound;
                    return response;
                }

                restaurant.CurrencyId = currency.Id;
                restaurant.Currency = currency;
            }

            if (restaurant.CountryId != restaurantEditDto.CountryId)
            {
                var country = await _countryRepository.GetCountryById(restaurantEditDto.CountryId);
                if (country == null)
                {
                    response.Status = ResponseStatus.NotFound;
                    return response;
                }

                restaurant.CountryId = country.Id;
                restaurant.Country = country;
            }

            restaurant.Address = restaurantEditDto.Address;
            restaurant.City = restaurantEditDto.City;
            restaurant.Description = restaurantEditDto.Description;
            restaurant.FacebookUrl = restaurantEditDto.FacebookUrl;
            restaurant.InstagramUrl = restaurantEditDto.InstagramUrl;
            restaurant.WebsiteUrl = restaurantEditDto.WebsiteUrl;
            restaurant.IsActive = restaurantEditDto.IsActive;
            restaurant.Name = restaurantEditDto.Name;
            restaurant.PhoneNumber = restaurantEditDto.PhoneNumber;
            restaurant.PostalCode = restaurantEditDto.PostalCode;

            if (!await _restaurantRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update restaurant";
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

    public async Task<Response<int>> Delete(int restaurantId)
    {
        Response<int> response = new();
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

            restaurant.IsDeleted = true;

            if (!await _restaurantRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete restaurant.";
            }

            response.Status = ResponseStatus.Success;
            response.Data = restaurant.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<RestaurantDetailsDto>> GetEmployeeRestaurantDetailsDto()
    {
        Response<RestaurantDetailsDto> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var restaurant = await _restaurantRepository.GetEmployeeRestaurantDetailsDto(employee.RestaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = restaurant;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<RestaurantCardDto>>> GetCustomerRestaurants(string sq)
    {
        Response<ICollection<RestaurantCardDto>> response = new();
        try
        {
            var restaurants = await _restaurantRepository.GetCustomerRestaurants(sq);
            response.Status = ResponseStatus.Success;
            response.Data = restaurants;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<CustomerRestaurantDetailsDto>> GetCustomerRestaurant(int restaurantId)
    {
        Response<CustomerRestaurantDetailsDto> response = new();
        try
        {
            var restaurant = await _restaurantRepository.GetCustomerRestaurant(restaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = restaurant;
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
