﻿using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;

namespace API.Controllers.EmployeeControllers;

[Authorize]
public class AccountController(
    IEmployeeService employeeService
    ) : DefaultEmployeeController
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpGet("get-account-details")]
    public async Task<ActionResult<EmployeeDtos.GetAccountDetailsDto>> GetAccountDetails()
    {
        var response = await _employeeService.GetAccountDetails();
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

    [HttpGet("get-account-edit")]
    public async Task<ActionResult<EmployeeDtos.GetAccountEditDto>> GetAccountEdit()
    {
        var response = await _employeeService.GetAccountEdit();
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

    [HttpPut("edit-account")]
    public async Task<ActionResult<EmployeeDtos.AccountDto>> EditAccount(EmployeeDtos.EditAccountDto editAccountDto)
    {
        var response = await _employeeService.EditAccount(editAccountDto);
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
