using APBD02.Service;

namespace APBD02.Domain.User;

public class Student : User
{
    public string StudentNumber { get; }
    public string Faculty { get; }
    
    public Student(int id, string firstName, string lastName, string studentNumber, string faculty)
        : base(id, firstName, lastName, RentalConfig.StudentRentalLimit)
    {
        StudentNumber = studentNumber;
        Faculty = faculty;
    }
}