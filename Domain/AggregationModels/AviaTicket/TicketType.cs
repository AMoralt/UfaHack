public class TicketType : Enumeration
{
    public static TicketType Econom = new(1, nameof(Econom));
    public static TicketType Business = new(2, nameof(Business));
    public TicketType(int id, string name) : base(id, name)
    {
        
    }
}