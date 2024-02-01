
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Azure.Storage;
using Azure.Storage.Blobs;

namespace API;

public class AppUserImageService(
    IAppUserImageRepository appUserImageRepository,
    IUserService userService,
    IConfiguration config,
    IEmployeeRepository employeeRepository,
    IAppUserRepository appUserRepository
    ) : IAppUserImageService
{
    private readonly IAppUserImageRepository _appUserImageRepository = appUserImageRepository;
    private readonly IUserService _userService = userService;
    private readonly IConfiguration _config = config;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IAppUserRepository _appUserRepository = appUserRepository;

    public async Task<Response<int>> DeleteEmployeeImage(int employeeId, int imageId)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var employee = await _employeeRepository.GetOwnerEmployee(employeeId, owner.Id);
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var user = await _appUserRepository.GetUserByUsername(employee.UniqueUsername);
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var userImage = await _appUserImageRepository.GetUserImage(imageId, user.Id);
            if (userImage == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            userImage.IsDeleted = true;
            if (!await _appUserImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete image.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = userImage.Id;

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<int>> DeleteImage(int imageId)
    {
        Response<int> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var image = await _appUserImageRepository.GetUserImage(imageId, user.Id);
            if (image == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            image.IsDeleted = true;
            if (!await _appUserImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete image.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = image.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ImageDto>> UploadEmployeeProfileImage(int employeeId, IFormFile image)
    {
        Response<ImageDto> response = new();
        try
        {
            if (!image.ContentType.Contains("image"))
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload only image.";
                return response;
            }
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var employee = await _employeeRepository.GetOwnerEmployee(employeeId, owner.Id);
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var user = await _appUserRepository.GetUserByUsername(employee.UniqueUsername);
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var currentDate = DateTime.UtcNow.ToString("dd-MM-yyyy");

            // Azure Storage
            var asAccount = _config["ASAccount"];
            var asKey = _config["ASAccountKey"];
            var creditential = new StorageSharedKeyCredential(asAccount, asKey);
            var accountUrl = $"https://{asAccount}.blob.core.windows.net";
            var blobServiceClient = new BlobServiceClient(new Uri(accountUrl), creditential);

            // container mora biti kreiran u Azure Storage
            var _userContainer = blobServiceClient.GetBlobContainerClient("user-images");
            //

            var uniqueImageName = $"{Guid.NewGuid()}-{image.FileName}";
            BlobClient client = _userContainer.GetBlobClient($"{currentDate}/{uniqueImageName}");
            await using (Stream data = image.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            var oldProfileImage = await _appUserImageRepository.GetProfileImage(user.Id);
            if (oldProfileImage != null)
            {
                oldProfileImage.IsDeleted = true;
            }


            AppUserImage userImage = new()
            {
                AppUserId = user.Id,
                AppUser = user,
                ContainerName = client.BlobContainerName,
                ContentType = image.ContentType,
                IsDeleted = false,
                Name = image.FileName,
                Size = image.Length,
                Type = AppUserImageType.Profile,
                UniqueName = uniqueImageName,
                Url = client.Uri.AbsoluteUri
            };

            _appUserImageRepository.AddImage(userImage);

            if (!await _appUserImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to upload profile image.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new ImageDto
            {
                Id = userImage.Id,
                Size = userImage.Size,
                Url = userImage.Url
            };


        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ImageDto>> UploadProfileImage(IFormFile image)
    {
        Response<ImageDto> response = new();
        try
        {
            if (!image.ContentType.Contains("image"))
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload only image.";
                return response;
            }

            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var currentDate = DateTime.UtcNow.ToString("dd-MM-yyyy");

            // Azure Storage
            var asAccount = _config["ASAccount"];
            var asKey = _config["ASAccountKey"];
            var creditential = new StorageSharedKeyCredential(asAccount, asKey);
            var accountUrl = $"https://{asAccount}.blob.core.windows.net";
            var blobServiceClient = new BlobServiceClient(new Uri(accountUrl), creditential);

            // container mora biti kreiran u Azure Storage
            var _userContainer = blobServiceClient.GetBlobContainerClient("user-images");
            //

            var uniqueImageName = $"{Guid.NewGuid()}-{image.FileName}";
            BlobClient client = _userContainer.GetBlobClient($"{currentDate}/{uniqueImageName}");
            await using (Stream data = image.OpenReadStream())
            {
                await client.UploadAsync(data);
            }

            var oldProfileImage = await _appUserImageRepository.GetProfileImage(user.Id);
            if (oldProfileImage != null)
            {
                oldProfileImage.IsDeleted = true;
            }

            AppUserImage userImage = new()
            {
                AppUserId = user.Id,
                AppUser = user,
                ContainerName = client.BlobContainerName,
                ContentType = image.ContentType,
                IsDeleted = false,
                Name = image.FileName,
                Size = image.Length,
                Type = AppUserImageType.Profile,
                UniqueName = uniqueImageName,
                Url = client.Uri.AbsoluteUri
            };

            _appUserImageRepository.AddImage(userImage);

            if (!await _appUserImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to upload profile image.";
                return response;
            };

            response.Status = ResponseStatus.Success;
            response.Data = new ImageDto
            {
                Id = userImage.Id,
                Size = userImage.Size,
                Url = userImage.Url
            };

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }
}
