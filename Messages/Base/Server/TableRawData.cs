using Messages.Base.Server.Base;
using ProtoBuf;

namespace Messages.Base.Server;

[ProtoContract]
public sealed class TableRawData : ServerMessage
{
    [ProtoMember(1)]
    public string JsonRawData { get; set; }
}