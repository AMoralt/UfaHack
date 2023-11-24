namespace Application.Exception;

public class UserAlreadyExistsException : System.Exception
{
    public UserAlreadyExistsException(string message) : base(message)
    {
        
    }
}