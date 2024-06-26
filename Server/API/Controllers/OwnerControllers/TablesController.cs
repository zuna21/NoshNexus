﻿using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.DTOs.OwnerDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace API;

[Authorize]
public class TablesController : DefaultOwnerController
{
    private readonly ITableService _tableService;
    public TablesController(
        ITableService tableService
    )
    {
        _tableService = tableService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<bool>> Create(ICollection<OwnerDtos.TableCardDto> tableCardDtos)
    {
        var response = await _tableService.CreateTables(tableCardDtos);
        switch (response.Status)
        {   
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var response = await _tableService.Delete(id);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<bool>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<bool>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<bool>)response.Data,
            _ => (ActionResult<bool>)BadRequest("Something went wrong."),
        };
    }


    [HttpGet("get-tables")]
    public async Task<ActionResult<ICollection<OwnerDtos.TableCardDto>>> GetTables([FromQuery] OwnerQueryParams.TablesQueryParams tablesQueryParams)
    {
        var response = await _tableService.GetTables(tablesQueryParams);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong");
        }
    }

    [HttpGet("get-all-restaurant-table-names/{restaurantId}")]
    public async Task<ActionResult<ICollection<OwnerDtos.TableDto>>> GetAllRestaurantTableNames(int restaurantId)
    {
        var response = await _tableService.GetAllRestaurantTableNames(restaurantId);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-restaurant-table-qr-codes/{restaurantId}")]
    public async Task<ActionResult<List<List<GetTableQrCodeDto>>>> GetRestaurantTableQrCodes(int restaurantId)
    {
        var response = await _tableService.GetRestaurantTableQrCodes(restaurantId);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
