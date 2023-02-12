using Google.Protobuf;
using Grpc.Core;
using Grpc.DataApi;
using Messages.Base;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Serializer = Messages.Serializer;

namespace Server.GrpcServer;

public sealed class NormalizaionTableApiServer : NormalizationTableApi.NormalizationTableApiBase
{
    public Message? Answer { get; set; }
    public Message? Input { get; set; }
    
    [Authorize(AuthenticationSchemes = CertificateAuthenticationDefaults.AuthenticationScheme)]
    public override Task<RetReply> SendDataMessage(ArgRequest request, ServerCallContext context)
    {
        Answer = null;
        var inputMsg = request.Message.ToByteArray();
        Input = Serializer.DeSerialize(inputMsg);
        
        SpinWait.SpinUntil(() => Answer is not null);

        var ret = new RetReply()
        {
            Message = ByteString.CopyFrom(Serializer.Serialize(Answer!))
        };
        
        return Task.FromResult(ret);
    }
}