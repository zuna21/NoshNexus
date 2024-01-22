using ApplicationCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;

namespace API;

[Authorize]
public class ReviewsController(
    IRestaurantReviewService restaurantReviewService
) : DefaultCustomerController
{
    private readonly IRestaurantReviewService _restaurantReviewService = restaurantReviewService;

    [HttpPost("create-review/{restaurantId}")]
    public async Task<ActionResult<RestaurantReviewDto>> CreateReview(int restaurantId, CreateRestaurantReviewDto createRestaurantReviewDto)
    {
        var response = await _restaurantReviewService.CreateReview(restaurantId, createRestaurantReviewDto);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
