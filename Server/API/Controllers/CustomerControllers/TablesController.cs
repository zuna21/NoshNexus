using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

namespace API.Controllers.CustomerControllers;

public class TablesController(ITableService tableService) : DefaultCustomerController
{
    private readonly ITableService _tableService = tableService;

    [HttpGet("get-tables/{restaurantId}")]
    public async Task<ActionResult<ICollection<CustomerDtos.TableDto>>> GetTables(int restaurantId)
    {
        var response = await _tableService.GetRestaurantTables(restaurantId);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
