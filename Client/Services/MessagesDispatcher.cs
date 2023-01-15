using System.Text.Json;
using Client.TableNormalization.RentApartment;
using Client.TableNormalization.RentApartment.ORMcontext;
using Messages.Base;
using Messages.Base.Server;

namespace Client;

public sealed class MessagesDispatcher
{

    public void DispatchMessage(Message message)
    {
        switch (message)
        {
            case TableInfoMessage tableInfoMessage:
                NormalizeTableMessage(tableInfoMessage);
                break;
            case TableRawData tableRawData:
                WriteRowToDb(tableRawData);
                break;
        }
    }

    private  void WriteRowToDb(TableRawData tableRawData)
    {
        var data = JsonSerializer.Deserialize<RentApartmentTuple>(tableRawData.JsonRawData);
        var rentApartmentContext = new RentApartmentsContext();
        var client = new Clients
        {
            FIO = data.FIO,
            Address = data.ClientAdress,
            PassportId = data.ClientPassportId,
            PhoneNumber = data.ClientPhoneNumber
        };
        var category = new FlatCategories
        {
            FlatCategoryId = data.FlatCategoryId,
            FlatCategory = data.FlatCategory
        };

        var flat = new Flats
        {
            Id = data.FlatId,
            Address = data.FlatAdress,
            FlatCategoryId = category,
            Floor = data.Floor,
            RoomsNumber = data.RoomsNumber,
            Square = data.FlatSquare
        };
        var contract = new Contracts
        {
            ContractId = data.ContractId,
            ClientPassportId = client,
            ContractTime = data.ContractTime
        };
        
        var id = rentApartmentContext.Apartments.Any() ? rentApartmentContext.Apartments.Select(a=>a.Id).Max() + 1 : 1;
        var rentApartment = new RentApartment
        {
            Id = id,
            ContractId = contract,
            FlatId = flat,
            MonthsAmount = data.MonthsAmount,
            RentCost = data.RentCost,
            StartRentMonth = data.StartRentMonth,
            YearRentMonth = data.YearRentMonth,
        };

        if (!rentApartmentContext.Flats.Any(f => f.Id == flat.Id))
            rentApartmentContext.Flats.Add(flat);
        else rentApartmentContext.Update(flat);

        if (!rentApartmentContext.FlatCategories.Any(c => c.FlatCategoryId == category.FlatCategoryId))
            rentApartmentContext.FlatCategories.Add(category);
        else rentApartmentContext.Update(category);
        
        if (!rentApartmentContext.Clients.Any(c => c.PassportId == client.PassportId))
            rentApartmentContext.Clients.Add(client);
        else rentApartmentContext.Update(client);

        if (!rentApartmentContext.Contracts.Any(c => c.ContractId == contract.ContractId))
            rentApartmentContext.Contracts.Add(contract);

        if (!rentApartmentContext.Apartments.Any(c => c.Id == rentApartment.Id))
            rentApartmentContext.Apartments.Add(rentApartment);


        rentApartmentContext.SaveChanges();
        rentApartmentContext.Dispose();
    }

    private void  NormalizeTableMessage(TableInfoMessage tableInfoMessage)
    {
        switch (tableInfoMessage.Name)
        {
            case "RentApartment":
                var context = new RentApartmentsContext();
                context.Database.EnsureCreated();
                context.Dispose();
                break;
        }
    }
    
}