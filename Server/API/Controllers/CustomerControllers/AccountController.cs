﻿using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

public class AccountController : DefaultCustomerController
{
    private readonly ICustomerService _customerService;
    public AccountController(
        ICustomerService customerService
    )
    {
        _customerService = customerService;
    }


    [HttpPost("register")]
    public async Task<ActionResult<CustomerDto>> Register(RegisterCustomerDto registerCustomerDto)
    {
        var response = await _customerService.Register(registerCustomerDto);
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
}