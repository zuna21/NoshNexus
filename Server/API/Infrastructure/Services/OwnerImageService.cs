
using ApplicationCore.DTOs;

namespace API;

public class OwnerImageService : IOwnerImageService
{
    private readonly IOwnerImageRepository _ownerImageRepository;
    private readonly IHostEnvironment _env;
    private readonly IOwnerService _ownerService;
    public OwnerImageService(
        IOwnerImageRepository ownerImageRepository,
        IHostEnvironment hostEnvironment,
        IOwnerService ownerService
    )
    {
        _ownerImageRepository = ownerImageRepository;
        _env = hostEnvironment;
        _ownerService = ownerService;
    }
    public async Task<Response<ImageDto>> UploadProfileImage(IFormFile image)
    {
        Response<ImageDto> response = new();
        try
        {
            if (image == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload image.";
                return response;
            }

            var fileType = Path.GetExtension(image.FileName);
            if (fileType.ToLower() != ".jpg" && fileType.ToLower() != ".png" && fileType.ToLower() != ".jpeg")
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please upload only image.";
                return response;
            }

            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }


            var currentPath = _env.ContentRootPath;  // Ovo je ...../Server/API/
            string directoryName = DateTime.Now.ToString("dd-MM-yyyy");
            var fullPath = Path.Combine(currentPath, "wwwroot", "images", "owner", directoryName);
            var relativePath = Path.Combine("images", "owner", directoryName);

            Directory.CreateDirectory(fullPath);
            var ownerImage = new OwnerImage
            {
                Name = image.FileName,
                ContentType = image.ContentType,
                Size = image.Length,
                UniqueName = $"{Guid.NewGuid()}-{image.FileName}",
                Type = OwnerImageType.Profile,
                Owner = owner,
                OwnerId = owner.Id
            };
            ownerImage.RelativePath = Path.Combine(relativePath, ownerImage.UniqueName);
            ownerImage.FullPath = Path.Combine(fullPath, ownerImage.UniqueName);
            ownerImage.Url = Path.Combine("http://localhost:5000", ownerImage.RelativePath);

            _ownerImageRepository.AddImage(ownerImage);
            using var stream = new FileStream(Path.Combine(fullPath, ownerImage.UniqueName), FileMode.Create);
            await image.CopyToAsync(stream);

            var oldProfileImageEntity = await _ownerImageRepository.GetProfileImage(owner.Id);
            if (oldProfileImageEntity != null)
            {
                oldProfileImageEntity.Type = OwnerImageType.Gallery;
            }

            if (!await _ownerImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to save profile images.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new ImageDto
            {
                Id = ownerImage.Id,
                Size = ownerImage.Size,
                Url = ownerImage.Url
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
