namespace APBD02.Domain.User;

public abstract class User
{
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    
    public string FullName => $"{FirstName} {LastName}";
    
    protected User(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
}