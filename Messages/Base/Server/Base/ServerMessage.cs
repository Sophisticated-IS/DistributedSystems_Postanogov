using ProtoBuf;

namespace Messages.Base.Server.Base;
[ProtoContract]
[ProtoInclude(4, typeof(TableInfoMessage))]
[ProtoInclude(5, typeof(EndStreamMessage))]
[ProtoInclude(7, typeof(TableRawData))]
public abstract class ServerMessage : Message
{
    
}