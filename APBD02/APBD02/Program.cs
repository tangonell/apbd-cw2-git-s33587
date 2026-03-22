// See https://aka.ms/new-console-template for more information

using APBD02.Domain.Equipment;
using APBD02.Domain.Rental;
using APBD02.Domain.User;
using APBD02.Result;
using APBD02.Service;

var userService = new UserService();
var equipmentService = new EquipmentService();
var rentalService = new RentalService(equipmentService, userService);

Console.WriteLine("=== 1-4. Add users and equipment, print ===");
Console.WriteLine("All users:");
userService.AddUser(id => new Employee(id, "John", "Doe", "IT", "Developer"));
userService.AddUser(id => new Student(id, "Jan", "Kowalski", "123456", "Computer Science"));
userService.AddUser(id => new Employee(id, "Alice", "Alice", "IT", "TA"));
PrintUsers(userService.GetAll());
Console.WriteLine();

Console.WriteLine("All equipment:");
equipmentService.AddEquipment(id => new Camera(id, "Canon EOS R5", 45, 5.0));
equipmentService.AddEquipment(id => new Laptop(id, "Dell Latitude", "Intel", "NVIDIA", 16, 512));
equipmentService.AddEquipment(id => new Projector(id, "Projector A", 3800, "1920x1080"));
PrintEquipment(equipmentService.GetAll());
Console.WriteLine();

Console.WriteLine("Renting camera to Jan for 3 days...");
rentalService.RentEquipment(2, 1, DateTime.Now, 3);
Console.WriteLine("Renting projector to Jan for 3 days (5 days earlier)...");
rentalService.RentEquipment(2, 3, DateTime.Now.AddDays(-5), 3);
Console.WriteLine();

Console.WriteLine("Available equipment:");
PrintEquipment(equipmentService.GetAvailable());
Console.WriteLine();

Console.WriteLine("Trying to rent laptop to Jan...");
var rentalResult = rentalService.RentEquipment(2, 2, DateTime.Now, 3);
if (rentalResult is Result<Rental, string>.Err err)
{
    Console.WriteLine(err.Error);
}
Console.WriteLine();

Console.WriteLine("Available equipment:");
PrintEquipment(equipmentService.GetAvailable());
Console.WriteLine();

Console.WriteLine("Overdue rentals:");
PrintRentals(rentalService.GetOverdueRentals());
Console.WriteLine();

Console.WriteLine("Returning the projector rented to Jan...");
var penalty = rentalService.ReturnEquipment(2, DateTime.Now);
Console.WriteLine($"Late penalty: {penalty.Unwrap()}");
Console.WriteLine();

Console.WriteLine("Marking returned projector as unavailable...");
var projector = equipmentService.GetById(3);
projector!.SetStatus(EquipmentStatus.Unavailable);
Console.WriteLine("All equipment:");
PrintEquipment(equipmentService.GetAll());
Console.WriteLine();

Console.WriteLine("Equipment rented to Jan:"); 
PrintRentals(rentalService.GetActiveRentalsForUser(2));
Console.WriteLine();

PrintReport(equipmentService.GetAll(), userService.GetAll(), rentalService.GetAll());


static void PrintUsers(IEnumerable<User> users)
{
    foreach (var user in users)
    {
        Console.WriteLine($"User #{user.Id}: {user.FullName}");
    }
}

static void PrintEquipment(IEnumerable<Equipment> equipments)
{
    foreach (var equipment in equipments)
    {
        Console.WriteLine(
            $"Equipment #{equipment.Id}: " +
            $"{equipment.Name} | " +
            $"Is available: {equipment.IsRentable} | " +
            $"Status: {equipment.Status}"
        );
    }
}

static void PrintRentals(IEnumerable<Rental> rentals)
{
    foreach (var rental in rentals)
        Console.WriteLine(
            $"Rental #{rental.Id}: " +
            $"User: {rental.User.FullName} | " +
            $"Equipment: {rental.Equipment.Name} | " +
            $"Due: {rental.DueDate:d} | " +
            $"Days overdue: {rental.DaysOverdue}"
        );
}

static void PrintReport(IEnumerable<Equipment> equipment, IEnumerable<User> users, IEnumerable<Rental> rentals)
{
    Console.WriteLine("=== Equipment Rental Report ===");
    Console.WriteLine("Equipment:");
    PrintEquipment(equipment);
    Console.WriteLine("Users:");
    PrintUsers(users);
    Console.WriteLine("Rentals:");
    PrintRentals(rentals);
}