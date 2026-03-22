namespace APBD02.Domain.Rental;

using Domain.User;
using Domain.Equipment;

public class Rental
{
    public int Id { get; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentalDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    // Penalty is a fixed sum per overdue day
    public decimal Penalty { get; }
    
    public bool IsActive => ReturnDate == null;
    
    // If not overdue, return 0
    public int DaysOverdue => 
        // ReturnDate cannot be null if the rental is not active; safe to suppress warning with '!'
        Math.Max(0, ((IsActive ? DateTime.Now : ReturnDate!.Value) - DueDate).Days);
    
    public Rental(
        int id,
        User user, Equipment equipment,
        DateTime rentalDate, DateTime dueDate,
        int penalty
    )
    {
        Id = id;
        User = user;
        Equipment = equipment;
        RentalDate = rentalDate;
        DueDate = dueDate;
        ReturnDate = null;
        Penalty = penalty;
    }

    public void Return(DateTime returnDate)
    {
        ReturnDate = returnDate;
    }
}