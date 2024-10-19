namespace Catalog.Domain.Exceptions;

public class NotFoundException : Exception, IResultTypeException
{
    public NotFoundException() : base("Item not found!")
    {
    }

    public NotFoundException(string message) : base(message)
    {
        
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}