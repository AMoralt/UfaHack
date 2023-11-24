public class AviaTicket : Entity
{
    public Status Status { get;}
    public Ticket Ticket { get;}
    public Size Size { get; private set; }
    
    public AviaTicket(Status status, Ticket ticket, Size size)
    {
        Status = status;
        Ticket = ticket;
        SetSize(size);
    }

    public void SetSize(Size size)
    {
        if (size is not null && 
            Ticket.TicketType.Equals(TicketType.Econom))
            Size = size;
        else
            Console.WriteLine("smth");
    }
    
    public void GiveOut()
    {
        
    }
}