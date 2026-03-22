namespace APBD02.Service;

using Domain.Equipment;

public class EquipmentService
{
    private readonly List<Equipment> _equipment = [];
    private int _nextId = 1;

    // A factory function is used here because I literally can't think of any better option 
    public T AddEquipment<T>(Func<int, T> equipmentFactory) 
        where T : Equipment
    {
        var equipment = equipmentFactory(_nextId);
        _equipment.Add(equipment);
        _nextId++;

        return equipment;
    }

    public Equipment? GetById(int id) => _equipment.FirstOrDefault(e => e.Id == id);

    public IEnumerable<Equipment> GetAvailable() => _equipment.Where(e => e.IsRentable);
}