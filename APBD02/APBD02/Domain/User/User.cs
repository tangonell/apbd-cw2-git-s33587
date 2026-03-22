namespace APBD02.Domain.User;

public abstract class User
{
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public int MaxRentals { get; }
    
    public string FullName => $"{FirstName} {LastName}";
    
    protected User(int id, string firstName, string lastName, int maxRentals)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MaxRentals = maxRentals;
    }
}