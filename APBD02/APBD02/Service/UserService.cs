namespace APBD02.Service;

using System.Collections.ObjectModel;
using Domain.User;

public class UserService
{
    private readonly List<User> _users = [];
    private int _nextId = 1;

    public T AddUser<T>(Func<int, T> userFactory)
        where T : User
    {
        var user = userFactory(_nextId);
        _users.Add(user);
        _nextId++;
        
        return user;
    }
    
    public User? GetById(int id) => _users.FirstOrDefault(user => user.Id == id);
    
    public ReadOnlyCollection<User> GetAll() => _users.AsReadOnly();
}