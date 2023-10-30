﻿
namespace API;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IOwnerService _ownerService;
    public RestaurantService(
        IRestaurantRepository restaurantRepository,
        IOwnerService ownerService
    )
    {
        _restaurantRepository = restaurantRepository;
        _ownerService = ownerService;
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

            var restaurant = new Restaurant
            {
                OwnerId = owner.Id,
                Owner = owner,
                Address = createRestaurantDto.Address,
                City = createRestaurantDto.City,
                Description = createRestaurantDto.Description,
                FacebookUrl = createRestaurantDto.FacebookUrl,
                InstagramUrl = createRestaurantDto.InstagramUrl,
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
}
