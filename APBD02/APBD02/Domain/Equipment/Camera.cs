namespace APBD02.Domain.Equipment;

public class Camera : Equipment
{
    public double Megapixels { get; }
    public double MaxZoom { get; }
    
    public Camera(int id, string name, double megapixels, double maxZoom) : base(id, name)
    {
        Megapixels = megapixels;
        MaxZoom = maxZoom;
    }
}