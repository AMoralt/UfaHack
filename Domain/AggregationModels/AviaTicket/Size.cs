
public class Size : Enumeration
{
    public static Size XS = new(1, nameof(XS));
    public static Size XL = new(2, nameof(XL));
    
    public Size(int id, string name) : base(id, name)
    {
        
    }
}