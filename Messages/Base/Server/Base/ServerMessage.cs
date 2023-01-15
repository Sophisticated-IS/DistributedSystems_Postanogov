using ProtoBuf;

namespace Messages.Base.Server.Base;
[ProtoContract]
[ProtoInclude(6, typeof(TableInfoMessage))]
[ProtoInclude(7, typeof(EndStreamMessage))]
[ProtoInclude(8, typeof(TableRawData))]
public abstract class ServerMessage : Message
{
    
}