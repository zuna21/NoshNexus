using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace API;

[Authorize]
public class OwnersController : DefaultOwnerController
{
    private readonly IOwnerService _ownerService;
    private readonly IOwnerImageService _ownerImageService;
    public OwnersController(
        IOwnerService ownerService,
        IOwnerImageService ownerImageService
    )
    {
        _ownerService = ownerService;
        _ownerImageService = ownerImageService;
    }

    [HttpGet("get-owner-edit")]
    public async Task<ActionResult<OwnerDtos.GetAccountEditDto>> GetOwnerEdit()
    {
        var response = await _ownerService.GetOwnerEdit();
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


    [HttpPut("update")]
    public async Task<ActionResult<int>> Update(OwnerDtos.EditAccountDto editOwnerDto)
    {
        var response = await _ownerService.Update(editOwnerDto);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<int>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<int>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<int>)response.Data,
            _ => (ActionResult<int>)BadRequest("Something went wrong."),
        };
    }


    [HttpGet("get-owner")]
    public async Task<ActionResult<OwnerDtos.GetAccountDetailsDto>> GetOwner()
    {
        var response = await _ownerService.GetOwnerDetails();
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

    [HttpPost("upload-profile-image")]
    public async Task<ActionResult<ImageDto>> UploadProfileImage()
    {
        var image = Request.Form.Files[0];
        var response = await _ownerImageService.UploadProfileImage(image);
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
