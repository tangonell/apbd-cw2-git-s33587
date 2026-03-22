namespace APBD02.Domain.Equipment;

using Result;

public abstract class Equipment
{
    public int Id { get; }
    public string Name { get; }
    public EquipmentStatus Status { get; private set; }
    
    public bool IsRentable => Status == EquipmentStatus.Available;
    
    protected Equipment(int id, string name)
    {
        Id = id;
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public void SetStatus(EquipmentStatus status)
    {
        Status = status;
    }
}