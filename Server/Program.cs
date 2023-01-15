using Server;

//todo run kafka  
//ZooKepeer
//C:\kafka_2.12-3.3.1\bin\windows>start zookeeper-server-start.bat C:\kafka_2.12-3.3.1\config\zookeeper.properties
//Kafka server
//C:\kafka_2.12-3.3.1\bin\windows>start kafka-server-start.bat C:\kafka_2.12-3.3.1\config\server.properties

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();