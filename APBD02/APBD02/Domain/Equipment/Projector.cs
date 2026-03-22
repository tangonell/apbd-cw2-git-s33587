namespace APBD02.Domain.Equipment;

public class Projector : Equipment
{
    public int Brightness { get; }
    public int Resolution { get; }
    
    public Projector(int id, string name, int brightness, int resolution) : base(id, name)
    {
        Brightness = brightness;
        Resolution = resolution;
    }
}