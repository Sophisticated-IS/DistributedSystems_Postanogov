using System.Text.Json;
using Messages;
using Messages.Base.Client;
using Messages.Base.Server;
using Server.Models;

namespace Server;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private CommunicationModule _communicationModule;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    public override  Task StartAsync(CancellationToken cancellationToken)
    {
         using var appContext = new ApplicationContext();

        if (!appContext.Apartments.Any())
        {
            appContext.Apartments.AddAsync(new RentApartment
            {
                ContractId = 1,
                ClientAdress = "Пермь, Свирязева 13",
                ClientPassportId = "5714 892021",
                ClientPhoneNumber = "89124932833",
                ContractTime = DateTime.Now.AddHours(1),
                FIO = "Бурунов Александр Викторовичев",
                FlatAdress = "Пермь, Советской армии 60",
                FlatCategory = "эконом-класс",
                FlatCategoryId = 1,
                FlatId = 1,
                FlatSquare = 25,
                Floor = 10,
                RoomsNumber = 1,
                StartRentMonth = 1,
                YearRentMonth = 2022,
                MonthsAmount = 9,
                RentCost = 600
            },cancellationToken);
            appContext.Apartments.AddAsync(new RentApartment
            {
                ContractId = 2,
                ClientAdress = "Пермь, Свирязева 13",
                ClientPassportId = "5714 892021",
                ClientPhoneNumber = "89124932833",
                ContractTime = DateTime.Now.AddHours(2),
                FIO = "Бурунов Александр Викторовичев",
                FlatAdress = "Пермь, Петропавловской 10А",
                FlatCategory = "бизнес-класс",
                FlatCategoryId = 3,
                FlatId = 2,
                FlatSquare = 80,
                Floor = 25,
                RoomsNumber = 5,
                StartRentMonth = 1,
                YearRentMonth = 2022,
                MonthsAmount = 3,
                RentCost = 1200
            },cancellationToken);
            appContext.Apartments.AddAsync(new RentApartment
            {
                ContractId = 3,
                ClientAdress = "Пермь, Михайлова 13",
                ClientPassportId = "5714 123456",
                ClientPhoneNumber = "89124932833",
                ContractTime = DateTime.Now.AddHours(3),
                FIO = "Жданов Максим Игоревич",
                FlatAdress = "Пермь, Крупской 30",
                FlatCategory = "комфорт",
                FlatCategoryId = 2,
                FlatId = 1,
                FlatSquare = 40,
                Floor = 1,
                RoomsNumber = 3,
                StartRentMonth = 1,
                YearRentMonth = 2022,
                MonthsAmount = 24,
                RentCost = 800
            },cancellationToken);
            appContext.SaveChangesAsync(cancellationToken);    
        }
        

        _communicationModule = new SocketCommunicationModule();
        _communicationModule.ConnectAsync(cancellationToken);

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker socket running at: {Time}", DateTimeOffset.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            var msg = await _communicationModule.GetMessageAsync(stoppingToken);
            _logger.LogInformation($"Server received message: {msg}");
            switch (msg)
            {
                case NormalizeTableMessage normalizeTableMessage:
                    var tableInfoMessage = new TableInfoMessage
                    {
                        Name = nameof(RentApartment),
                        JsonSchema = JsonSerializer.Serialize(new RentApartment())
                    };
                    await _communicationModule.SendMessageAsync(tableInfoMessage, stoppingToken);
                    break;
                
                case StartStreamMessage:
                    var appContext = new ApplicationContext();
                    foreach (var apartment in appContext.Apartments)
                    {
                        var data = JsonSerializer.Serialize(apartment);
                        var rawMsg = new TableRawData
                        {
                            JsonRawData = data
                        };
                        await _communicationModule.SendMessageAsync(rawMsg, stoppingToken);
                       
                        //ждём подтверждения получения сообщения
                        await _communicationModule.GetMessageAsync(stoppingToken);
                    }
                    await _communicationModule.SendMessageAsync(new EndStreamMessage(), stoppingToken);
                    
                    break;
            }
            
            
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _communicationModule.DisconnectAsync(cancellationToken);
        return base.StopAsync(cancellationToken);
    }
}