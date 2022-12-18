using Messages.Base;

namespace Messages;

public abstract class CommunicationModule
{
    public abstract Task ConnectAsync(CancellationToken cancellationToken);

    public abstract Task<Message> GetMessageAsync(CancellationToken cancellationToken);

    public abstract Task SendMessageAsync(Message message, CancellationToken cancellationToken);
    public abstract Task DisconnectAsync(CancellationToken cancellationToken);
}