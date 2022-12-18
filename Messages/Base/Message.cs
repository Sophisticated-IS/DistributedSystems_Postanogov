using Messages.Base.Client.Base;
using Messages.Base.Server.Base;
using ProtoBuf;

namespace Messages.Base;

[ProtoContract]
[ProtoInclude(1, typeof(ClientMessage))]
[ProtoInclude(2, typeof(ServerMessage))]
public abstract class Message
{
    
}