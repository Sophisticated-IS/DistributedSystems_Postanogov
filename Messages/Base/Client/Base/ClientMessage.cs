using ProtoBuf;

namespace Messages.Base.Client.Base;
[ProtoContract]
[ProtoInclude(3, typeof(NormalizeTableMessage))]
[ProtoInclude(6, typeof(StartStreamMessage))]
[ProtoInclude(8, typeof(ContinueStreamMessage))]
public abstract class ClientMessage : Message
{
    
}