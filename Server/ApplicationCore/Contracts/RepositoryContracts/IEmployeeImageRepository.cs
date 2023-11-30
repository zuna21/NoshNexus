using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IEmployeeImageRepository
{
    void AddImage(EmployeeImage image);
    Task<EmployeeImage> GetProfileImage(int employeeId);
    Task<bool> SaveAllAsync();
}

