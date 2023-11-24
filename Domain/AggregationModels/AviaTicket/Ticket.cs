public class Ticket : Entity
{
    public TicketType TicketType { get; }

    public Ticket(TicketType type)
    {
        TicketType = type;
    }
}