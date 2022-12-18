using Messages.Base.Server.Base;
using ProtoBuf;

namespace Messages.Base.Server;
[ProtoContract]
public sealed class TableInfoMessage : ServerMessage
{
    [ProtoMember(1)]
    public string Name { get; set; }
    [ProtoMember(2)]
    public string JsonSchema { get; set; }
}