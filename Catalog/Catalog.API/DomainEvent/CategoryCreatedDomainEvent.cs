using MediatR;

namespace Catalog.API.DomainEvent;

public class CategoryCreatedDomainEvent : INotification
{
    
}

public class CategoryCreatedDomainEventHandler : INotificationHandler<CategoryCreatedDomainEvent>
{
    public Task Handle(CategoryCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}