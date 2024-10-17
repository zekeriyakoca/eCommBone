namespace Catalog.Domain.Exceptions;

public class CatalogDomainException : Exception, IDomainException
{
    public CatalogDomainException() : base("Catalog Domain Exception Occurred")
    {
    }

    public CatalogDomainException(string message) : base(message)
    {
        
    }

    public CatalogDomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}