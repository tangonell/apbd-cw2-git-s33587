namespace APBD02.Domain.Equipment;

public abstract class Equipment
{
    public int Id { get; }
    public string Name { get; }
    public EquipmentStatus Status { get; }
    
    public bool IsRentable => Status == EquipmentStatus.Available;
    
    protected Equipment(int id, string name)
    {
        Id = id;
        Name = name;
        Status = EquipmentStatus.Available;
    }
}