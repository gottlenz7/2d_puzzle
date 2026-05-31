public class PrismModel
{
    public Light redLight;
    public Light blueLight;

    public bool HasRed => redLight != null;
    public bool HasBlue => blueLight != null;
    public bool IsEmpty => redLight == null && blueLight == null;
}
