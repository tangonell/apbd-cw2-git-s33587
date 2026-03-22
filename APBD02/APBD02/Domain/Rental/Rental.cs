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
    public DateTime? ReturnDate { get; }
    public int Penalty { get; }
    
    public Rental(
        int id,
        User user, Equipment equipment,
        DateTime rentalDate, DateTime dueDate, DateTime? returnDate,
        int penalty
    )
    {
        Id = id;
        User = user;
        Equipment = equipment;
        RentalDate = rentalDate;
        DueDate = dueDate;
        ReturnDate = returnDate;
    }
}