namespace ApplicationCore.DTOs.EmployeeDtos;

public class MenuDto
{

}

public class EditMenuDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}

public class GetMenuEditDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
