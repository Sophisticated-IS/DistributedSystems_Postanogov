using ProtoBuf;

namespace Messages.Base.Client.Base;
[ProtoContract]
[ProtoInclude(3, typeof(NormalizeTableMessage))]
[ProtoInclude(4, typeof(StartStreamMessage))]
[ProtoInclude(5, typeof(ContinueStreamMessage))]
public abstract class ClientMessage : Message
{
    
}