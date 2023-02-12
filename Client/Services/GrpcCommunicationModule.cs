using System.Security.Cryptography.X509Certificates;
using Google.Protobuf;
using Grpc.DataApi;
using Grpc.Net.Client;
using Messages;
using Messages.Base;

namespace Client;

public sealed class GrpcCommunicationModule : CommunicationModule
{
    private NormalizationTableApi.NormalizationTableApiClient? _client;
    private RetReply _lastResult;
    private GrpcChannel _channel;

    public override async Task ConnectAsync(CancellationToken cancellationToken)
    {
        _channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
        {
            HttpHandler = CreateHttpHandler(true)
        });

        _client = new NormalizationTableApi.NormalizationTableApiClient(_channel);
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
        _lastResult = await _client.SendDataMessageAsync(request);
    }

    static HttpClientHandler CreateHttpHandler(bool includeClientCertificate)
    {
        var handler = new HttpClientHandler();

        if (includeClientCertificate)
        {
            // Load client certificate
            var certPath = @"C:\Users\Igor SI\RiderProjects\DistributedSystems_PostanogovIS\Client\Services\client.pfx";
            var clientCertificate = new X509Certificate2(certPath, "1111");
            handler.ClientCertificates.Add(clientCertificate);
        }

        return handler;
    }

    public override async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        //todo как закрыть клиента?
        _client = null;
    }
}