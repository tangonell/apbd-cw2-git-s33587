# apbd-cw2-git-s33587

## Tutorial 2 - Object-Oriented Project in C#
> A console application focused on code structure, class responsibilities, and good design practices

### Project Description
This project is a simple equipment rental system written in C#. It models:
- `User` (`Student`, `Employee`)
- `Equipment` (`Camera`, `Laptop`, `Projector`)
- `Rental` (creating, tracking, returning, and overdue handling)

The app runs sample scenarios in `Program.cs` to demonstrate functional requirements.

## Project structure
The project is organized into logical packages according to DDD principles:
- `Domain/` - core domain entities
- `Service/` - execution/business logic,
- `Result/` - result type for explicit error handling
- `Program.cs` - main program

## Design Justification
- The model uses inheritance where it improves clarity:
  - `User` is a base class for `Student` and `Employee`.
  - `Equipment` is a base class for various equipment types.
- Business operations are kept in services:
  - `UserService` manages users.
  - `EquipmentService` manages equipment equipment.
  - `RentalService` coordinates `UserService` and `EquipmentService` to manage rental.
- `Result<T, E>` is used for error handling to make errors part of the contract, 
improving readability over regular exceptions.
    - This pattern is difficult to implement properly in C# without ADT/tagged union support,
    but it's possible, if not as ergonomic/elegant.
- `RentalConfig` centralizes basic business constants (limits, penalty value), which keeps rule changes in one place.

## Notes
- Data is stored in memory (no database/persistence).
- The current app only runs a predefined scenario, it isn't interactive.
- Optional features were omitted, mainly due to a lack of time (I had a 2-day conference to attend)
