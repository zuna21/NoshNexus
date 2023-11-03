namespace API;

public interface IEmployeeRepository
{
    void CreateEmployee(Employee employee);
    Task<ICollection<EmployeeCardDto>> GetEmployees(int ownerId);
    Task<GetEmployeeEditDto> GetEmployeeEdit(int employeeId, int ownerId);
    Task<EmployeeDetailsDto> GetEmployee(int employeeId, int ownerId);
    Task<bool> SaveAllAsync();
}
