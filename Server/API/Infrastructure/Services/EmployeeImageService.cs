
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public class EmployeeImageService : IEmployeeImageService
{
    private readonly IEmployeeImageRepository _employeeImageRepository;
    private readonly IEmployeeService _employeeService;
    private readonly IHostEnvironment _env;
    public EmployeeImageService(
        IEmployeeImageRepository employeeImageRepository,
        IEmployeeService employeeService,
        IHostEnvironment hostEnvironment
    )
    {
        _employeeImageRepository = employeeImageRepository;
        _employeeService = employeeService;
        _env = hostEnvironment;
    }
    public async Task<Response<ImageDto>> UploadProfileImage(int employeeId, IFormFile image)
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

            var employee = await _employeeService.GetOwnerEmployee(employeeId);
            if (employee == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var currentPath = _env.ContentRootPath;  // Ovo je ...../Server/API/
            string directoryName = DateTime.Now.ToString("dd-MM-yyyy");
            var fullPath = Path.Combine(currentPath, "wwwroot", "images", "employee", directoryName);
            var relativePath = Path.Combine("images", "employee", directoryName);
            

            Directory.CreateDirectory(fullPath);
            var employeeImage = new EmployeeImage
            {
                Name = image.FileName,
                ContentType = image.ContentType,
                Size = image.Length,
                UniqueName = $"{Guid.NewGuid()}-{image.FileName}",
                Type = EmployeeImageType.Profile,
                Employee = employee,
                EmployeeId = employee.Id
            };
            employeeImage.RelativePath = Path.Combine(relativePath, employeeImage.UniqueName);
            employeeImage.FullPath = Path.Combine(fullPath, employeeImage.UniqueName);
            employeeImage.Url = Path.Combine("http://localhost:5000", employeeImage.RelativePath);

            _employeeImageRepository.AddImage(employeeImage);
            using var stream = new FileStream(Path.Combine(fullPath, employeeImage.UniqueName), FileMode.Create);
            await image.CopyToAsync(stream);

            var oldProfileImageEntity = await _employeeImageRepository.GetProfileImage(employee.Id);
            if (oldProfileImageEntity != null)
            {
                oldProfileImageEntity.Type = EmployeeImageType.Gallery;
            }

            if (!await _employeeImageRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to save profile images.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new ImageDto
            {
                Id = employeeImage.Id,
                Size = employeeImage.Size,
                Url = employeeImage.Url
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
