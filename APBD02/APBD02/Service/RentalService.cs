namespace APBD02.Service;

using System.Collections.ObjectModel;
using Domain.Rental;
using Domain.Equipment;
using Domain.User;
using Result;

public class RentalService
{
    private int _nextId = 1;
    private readonly List<Rental> _rentals;
    private readonly EquipmentService _equipmentService;
    private readonly UserService _userService;
    
    public RentalService(EquipmentService equipmentService, UserService userService)
    {
        _rentals = [];
        _equipmentService = equipmentService;
        _userService = userService;
    }
    
    public ReadOnlyCollection<Rental> GetAllRentals() => _rentals.AsReadOnly();
    public Rental? GetById(int id) => _rentals.FirstOrDefault(r => r.Id == id);
    public IEnumerable<Rental> GetActiveRentals() => _rentals.Where(r => r.IsActive);
    public IEnumerable<Rental> GetActiveRentalsForUser(int userId) => _rentals
        .Where(r => r.IsActive && r.User.Id == userId);
    public IEnumerable<Rental> GetOverdueRentals() => _rentals.Where(r => r.DaysOverdue > 0);

    public Result<Rental, string> RentEquipment(int userId, int equipmentId, DateTime rentalDate, int rentalDays)
    {
        var equipmentResult = GetEquipmentById(equipmentId);
        if (equipmentResult is Result<Equipment, string>.Err equipemtnErr) return equipemtnErr.Error;
        var equipment = equipmentResult.Unwrap();
        
        var userResult = GetUserById(userId);
        if (userResult is Result<User, string>.Err userErr) return userErr.Error;
        var user = userResult.Unwrap();
        
        var activeRentals = _rentals.Count(r => r.IsActive && r.User.Id == user.Id);
        if (activeRentals >= user.MaxRentals)
            return $"User with id {userId} has reached the rental limit.";
        
        var rental = new Rental(
            _nextId++,
            user, equipment,
            rentalDate, rentalDate.AddDays(rentalDays),
            RentalConfig.RentalPenalty
        );
        _rentals.Add(rental);
        
        equipment.SetStatus(EquipmentStatus.Rented);

        return rental;
    }
    
    // this method returns the calculated penalty
    public Result<decimal, string> ReturnEquipment(int rentalId, DateTime returnDate)
    {
        var rentalResult = GetRentalById(rentalId);
        if (rentalResult is Result<Rental, string>.Err rentalErr) return rentalErr.Error;
        var rental = rentalResult.Unwrap();
        
        rental.Return(returnDate);
        rental.Equipment.SetStatus(EquipmentStatus.Available);
        _rentals.Remove(rental);
        return rental.Penalty * rental.DaysOverdue;
    }
    
    public ReadOnlyCollection<Rental> GetAll() => _rentals.AsReadOnly();

    private Result<Equipment, string> GetEquipmentById(int equipmentId)
    {
        var equipment = _equipmentService.GetById(equipmentId);
        if (equipment == null)
            return $"Equipment with id {equipmentId} not found";
        if (!equipment.IsRentable)
            return $"Equipment with id {equipmentId} is not available for rent.";
        return equipment;
    }

    private Result<User, string> GetUserById(int userId)
    {
        var user = _userService.GetById(userId);
        if (user == null)
            return $"User with id {userId} not found.";
        return user;
    }

    private Result<Rental, string> GetRentalById(int rentalId)
    {
        var rental = GetById(rentalId);
        if (rental == null)
            return $"Rental with id {rentalId} not found.";
        return rental;
    }
}