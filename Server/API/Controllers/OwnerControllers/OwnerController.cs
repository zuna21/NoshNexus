﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class OwnerController : OwnerDefaultController
{
    private readonly IOwnerService _ownerService;
    public OwnerController(
        IOwnerService ownerService
    )
    {
        _ownerService = ownerService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<OwnerAccountDto>> Register(RegisterOwnerDto registerOwnerDto)
    {
        var response = await _ownerService.Register(registerOwnerDto);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong");
        }
    }

    [HttpPost("login")]
    [Authorize]
    public async Task<ActionResult<OwnerAccountDto>> Login(LoginOwnerDto loginOwnerDto)
    {
        var response = await _ownerService.Login(loginOwnerDto);
        switch (response.Status)
        {
            case ResponseStatus.Unauthorized:
                return Unauthorized(response.Message);
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
