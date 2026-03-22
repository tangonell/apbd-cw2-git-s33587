namespace APBD02.Domain.Equipment;

public class Laptop : Equipment
{
    public string CPU { get; }
    public string GPU { get; }
    public int RAM { get; }
    public int Storage { get; }
    
    public Laptop(int id, string name, string cpu, string gpu, int ram, int storage) : base(id, name)
    {
        CPU = cpu;
        GPU = gpu;
        RAM = ram;
        Storage = storage;
    }
}