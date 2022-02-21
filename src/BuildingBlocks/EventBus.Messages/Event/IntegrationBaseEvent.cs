namespace EventBus.Messages.Event;

public class IntegrationBaseEvent
{
    public IntegrationBaseEvent()
    {
        Id = Guid.NewGuid();
        CreationDateTime = DateTime.Now;
    }
    public IntegrationBaseEvent(Guid id, DateTime creationDateTime)
    {
        Id = id;
        CreationDateTime = creationDateTime;
    }
    public Guid Id { get; private set; }
    public DateTime CreationDateTime { get; private set; }
}