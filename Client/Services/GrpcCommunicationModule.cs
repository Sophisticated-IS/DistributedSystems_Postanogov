using Google.Protobuf;
using Grpc.DataApi;
using Grpc.Net.Client;
using Messages;
using Messages.Base;

namespace Client;

public sealed class GrpcCommunicationModule: CommunicationModule
{
    private NormalizationTableApi.NormalizationTableApiClient? _client;
    private const string HttpLocalhost = "http://localhost:5665";
    private RetReply _lastResult;
    public override async Task ConnectAsync(CancellationToken cancellationToken)
    {
        //todo шифрование ?
        var channel = GrpcChannel.ForAddress(HttpLocalhost);
        _client = new NormalizationTableApi.NormalizationTableApiClient(channel);
    }

    public override Task<Message> GetMessageAsync(CancellationToken cancellationToken)
    {
        var msg = Serializer.DeSerialize(_lastResult.Message.ToByteArray());
        return Task.FromResult(msg);
    }

    public override async Task SendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var bytesMsg = Serializer.Serialize(message);
        var request = new ArgRequest()
        {
            Message = ByteString.CopyFrom(bytesMsg) 
        };
        _lastResult = await _client.SendDataMessageAsync(request).ConfigureAwait(false);
    }

    public override async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        //todo как закрыть клиента?
        _client = null;
    }
}