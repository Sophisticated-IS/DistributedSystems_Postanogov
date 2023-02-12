using Grpc.Core;
using Grpc.DataApi;
using Messages;
using Messages.Base;
using Server.GrpcServer;

namespace Server;

public sealed class GrpcCommunicationModule : CommunicationModule
{
    private Grpc.Core.Server _server;
    private NormalizaionTableApiServer _grpcService;

    public override async Task ConnectAsync(CancellationToken cancellationToken)
    {
         _grpcService = new NormalizaionTableApiServer();
         _server = new Grpc.Core.Server()
        {
            Services =
            {
                NormalizationTableApi.BindService(_grpcService)
            },
            Ports =
            {
                new ServerPort("[::]", 5665, ServerCredentials.Insecure)
            }
        };
        _server.Start();  
         

    }

    public override Task<Message> GetMessageAsync(CancellationToken cancellationToken)
    {
        SpinWait.SpinUntil(() => _grpcService.Input is null);
        var grpcServiceInput = _grpcService.Input;
        _grpcService.Input = null;

        return Task.FromResult(grpcServiceInput!);
    }

    public override Task SendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        _grpcService.Answer = message;
        return Task.CompletedTask;
    }

    public override Task DisconnectAsync(CancellationToken cancellationToken)
    {
        return _server.ShutdownAsync();
    }
}