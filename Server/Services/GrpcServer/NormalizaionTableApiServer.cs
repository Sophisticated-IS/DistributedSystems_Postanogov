using Google.Protobuf;
using Grpc.Core;
using Grpc.DataApi;
using Messages.Base;
using Serializer = Messages.Serializer;

namespace Server.GrpcServer;

public sealed class NormalizaionTableApiServer : NormalizationTableApi.NormalizationTableApiBase
{
    public Message? Answer { get; set; }
    public Message? Input { get; set; }
    
    public override Task<RetReply> SendDataMessage(ArgRequest request, ServerCallContext context)
    {
        Answer = null;
        var inputMsg = request.Message.ToByteArray();
        Input = Serializer.DeSerialize(inputMsg);
        
        SpinWait.SpinUntil(() => Answer is null);

        var ret = new RetReply()
        {
            Message = ByteString.CopyFrom(Serializer.Serialize(Answer!))
        };
        
        return Task.FromResult(ret);
    }
}