using System.Text;
using Confluent.Kafka;
using Messages;
using Messages.Base;
using Messages.Base.Client;

namespace Client;

public sealed class KafkaCommunicationModule : CommunicationModule
{
    private IProducer<Null, byte[]> _producer;

    public override async Task ConnectAsync(CancellationToken cancellationToken)
    {
        var config = new ProducerConfig { BootstrapServers = "DESKTOP-D9RH8HA:9092" };

        _producer = new ProducerBuilder<Null, byte[]>(config).Build();
    }

    public override async Task<Message> GetMessageAsync(CancellationToken cancellationToken)
    {
        var conf = new ConsumerConfig
        { 
            GroupId = "normalization-server-topic",
            BootstrapServers = "DESKTOP-D9RH8HA:9092",
            // Note: The AutoOffsetReset property determines the start offset in the event
            // there are not yet any committed offsets for the consumer group for the
            // topic/partitions of interest. By default, offsets are committed
            // automatically, so in this example, consumption will only start from the
            // earliest message in the topic 'my-topic' the first time you run the program.
            AutoOffsetReset = AutoOffsetReset.Latest, 
            EnableAutoOffsetStore = true,
            EnableAutoCommit = true
        };
        using  var consumer = new ConsumerBuilder<Ignore, byte[]>(conf).Build();
        consumer.Subscribe("normalization-server-topic");

        var consumeResult = await Task.Run(() => consumer.Consume(cancellationToken), cancellationToken);
        consumer.Unsubscribe();
        var msg = Serializer.DeSerialize(consumeResult.Message.Value); 
        return msg;
    }

    public override async Task SendMessageAsync(Message message, CancellationToken cancellationToken)
    {
        var bytesMsg = Serializer.Serialize(message);
        await _producer.ProduceAsync("normalization-client-topic", new Message<Null, byte[]>
        {
            Value = bytesMsg
        }, cancellationToken).ConfigureAwait(false);
    }

    public override async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        _producer?.Dispose();
    }
}