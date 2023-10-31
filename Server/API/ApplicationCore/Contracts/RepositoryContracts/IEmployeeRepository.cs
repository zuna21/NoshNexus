namespace API;

public interface IEmployeeRepository
{
    void CreateEmployee(Employee employee);
    Task<bool> SaveAllAsync();
}
