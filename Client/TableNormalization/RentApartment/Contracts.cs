using System.ComponentModel.DataAnnotations;

namespace Client.TableNormalization.RentApartment;

public sealed class Contracts
{
    /// <summary>
    /// № договора об аренде
    /// </summary>
    [Key]  public uint ContractId { get; set; }

    /// <summary>
    /// Дата договора
    /// </summary>
    public DateTime ContractTime { get; set; }
    
    /// <summary>
    /// Номер паспорта клиента
    /// </summary>
    public Clients ClientPassportId { get; set; }
}