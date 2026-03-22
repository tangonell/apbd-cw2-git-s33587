namespace APBD02.Domain.Equipment;

public class Projector : Equipment
{
    public int Brightness { get; }
    public string Resolution { get; }
    
    public Projector(int id, string name, int brightness, string resolution) : base(id, name)
    {
        Brightness = brightness;
        Resolution = resolution;
    }
}