using APBD02.Service;

namespace APBD02.Domain.User;

public class Employee : User
{
    public string Department { get; }
    public string JobTitle { get; }
    
    public Employee(int id, string firstName, string lastName, string department, string jobTitle)
        : base(id, firstName, lastName, RentalConfig.EmployeeRentalLimit)
    {
        Department = department;
        JobTitle = jobTitle;
    }
}