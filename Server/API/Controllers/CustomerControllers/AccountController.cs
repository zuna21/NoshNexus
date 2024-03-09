using ApplicationCore;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.DTOs.CustomerDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

namespace API.Controllers.CustomerControllers;

public class AccountController(
    ICustomerService customerService,
    IAppUserImageService appUserImageService
    ) : DefaultCustomerController
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IAppUserImageService _appUserImageService = appUserImageService;

    [HttpGet("refresh-customer")]
    public async Task<ActionResult<AccountDto>> RefreshCustomer()
    {
        var response = await _customerService.RefreshCustomer();
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

    [HttpGet("login-as-guest")]
    public async Task<ActionResult<CustomerDtos.AccountDto>> LoginAsGuest()
    {
        var response = await _customerService.LoginAsGuest();
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<CustomerDtos.AccountDto>> Login(CustomerDtos.LoginDto loginCustomerDto)
    {
        var response = await _customerService.Login(loginCustomerDto);
        switch (response.Status)
        {
            case ResponseStatus.Unauthorized:
                return Unauthorized(response.Message);
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

    [Authorize]
    [HttpPost("activate-account")]
    public async Task<ActionResult<CustomerDtos.AccountDto>> ActivateAccount(CustomerDtos.ActivateAccountDto activateAccountDto)
    {
        var response = await _customerService.ActivateAccount(activateAccountDto);
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

    [Authorize]
    [HttpGet("get-account-details")]
    public async Task<ActionResult<GetAccountDetailsDto>> GetAccountDetails()
    {
        var response = await _customerService.GetAccountDetails();
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

    [Authorize]
    [HttpGet("get-account-edit")]
    public async Task<ActionResult<GetAccountEditDto>> GetAccountEdit()
    {
        var response = await _customerService.GetAccountEdit();
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

    [Authorize]
    [HttpPut("update-account")]
    public async Task<ActionResult<AccountDto>> UpdateAccount(EditAccountDto editAccountDto)
    {
        var response = await _customerService.EditAccount(editAccountDto);
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

    [Authorize]
    [HttpPost("upload-profile-image")]
    public async Task<ActionResult<ImageDto>> UploadProfileImage()
    {
        var image = Request.Form.Files[0];
        var response = await _appUserImageService.UploadProfileImage(image);
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

    [Authorize]
    [HttpPost("update-fcm-token")]
    public async Task<ActionResult<bool>> UpdateFcmToken(FcmTokenDto fcmToken)
    {
        var response = await _customerService.UpdateFcmToken(fcmToken);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong");
        }
    }

}
