namespace API;

public interface IEmployeeRepository
{
    void CreateEmployee(Employee employee);
    Task<ICollection<EmployeeCardDto>> GetEmployees(int ownerId);
    Task<bool> SaveAllAsync();
}
