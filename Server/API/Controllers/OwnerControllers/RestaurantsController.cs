using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class RestaurantsController : DefaultOwnerController
{
    private readonly IRestaurantService _restaurantService;
    private readonly IRestaurantImageService _restaurantImageService;
    public RestaurantsController(
        IRestaurantService restaurantService,
        IRestaurantImageService restaurantImageService
    )
    {
        _restaurantService = restaurantService;
        _restaurantImageService = restaurantImageService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<int>> Create(CreateRestaurantDto createRestaurantDto)
    {
        Response<int> response = await _restaurantService.Create(createRestaurantDto);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Unauthorized:
                return Unauthorized(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> Update(int id, RestaurantEditDto restaurantEditDto)
    {
        Response<bool> response = await _restaurantService.Update(id, restaurantEditDto);
        return response.Status switch
        {
            ResponseStatus.NotFound => NotFound(),
            ResponseStatus.BadRequest => BadRequest(response.Message),
            ResponseStatus.Success => Ok(),
            _ => BadRequest("Something went wrong."),
        };
    }

    [HttpGet("get-restaurants")]
    public async Task<ActionResult<ICollection<RestaurantCardDto>>> GetRestaurants()
    {
        Response<ICollection<RestaurantCardDto>> response = await _restaurantService.GetRestaurants();
        switch (response.Status)
        {
            case ResponseStatus.Unauthorized:
                return Unauthorized(response.Message);
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong");
        }
    }

    [HttpGet("get-restaurant/{id}")]
    public async Task<ActionResult<RestaurantDetailsDto>> GetRestaurant(int id)
    {
        Response<RestaurantDetailsDto> response = await _restaurantService.GetRestaurant(id);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong");
        }
    }

    [HttpGet("get-restaurants-for-select")]
    public async Task<ActionResult<ICollection<RestaurantSelectDto>>> GetRestaurantsForSelect()
    {
        Response<ICollection<RestaurantSelectDto>> response = await _restaurantService.GetRestaurantSelect();
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<ICollection<RestaurantSelectDto>>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<ICollection<RestaurantSelectDto>>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<ICollection<RestaurantSelectDto>>)Ok(response.Data),
            _ => (ActionResult<ICollection<RestaurantSelectDto>>)BadRequest("Something went wrong"),
        };
    }

    [HttpGet("get-restaurant-edit/{id}")]
    public async Task<ActionResult<GetRestaurantEditDto>> GetRestaurantEdit(int id)
    {
        var response = await _restaurantService.GetRestaurantEdit(id);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-restaurant-create")]
    public async Task<ActionResult<GetCreateRestaurantDto>> GetRestaurantCreate()
    {
        var response = await _restaurantService.GetCreateRestaurant();
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpPost("upload-profile-image/{id}")]
    public async Task<ActionResult<ChangeProfileImageDto>> UploadProfileImage(int id)
    {
        var image = Request.Form.Files[0];
        var response = await _restaurantImageService.UploadProfileImage(id, image);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<ChangeProfileImageDto>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<ChangeProfileImageDto>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<ChangeProfileImageDto>)response.Data,
            _ => (ActionResult<ChangeProfileImageDto>)BadRequest("Something went wrong."),
        };
    }

    [HttpPost("upload-images/{id}")]
    public async Task<ActionResult<ICollection<ImageDto>>> UploadImages(int id)
    {
        var images = Request.Form.Files;
        var response = await _restaurantImageService.UploadImages(id, images);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<ICollection<ImageDto>>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<ICollection<ImageDto>>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<ICollection<ImageDto>>)Ok(response.Data),
            _ => (ActionResult<ICollection<ImageDto>>)BadRequest("Something went wrong."),
        };
    }

    [HttpDelete("delete-image/{restaurantId}/{imageId}")]
    public async Task<ActionResult> Delete(int restaurantId, int imageId)
    {
        var response = await _restaurantImageService.Delete(restaurantId, imageId);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return NoContent();
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
