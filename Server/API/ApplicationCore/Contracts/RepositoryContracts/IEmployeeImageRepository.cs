using ApplicationCore.Entities;

namespace API;

public interface IEmployeeImageRepository
{
    void AddImage(EmployeeImage image);
    Task<EmployeeImage> GetProfileImage(int employeeId);
    Task<bool> SaveAllAsync();
}
