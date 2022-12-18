using Messages.Base.Client.Base;
using ProtoBuf;

namespace Messages.Base.Client;

/// <summary>
/// Сообщение запроса нормализации таблицы
/// </summary>
[ProtoContract]
public sealed class NormalizeTableMessage : ClientMessage
{
    [ProtoMember(1)]
    public string TableName { get; set; }
}