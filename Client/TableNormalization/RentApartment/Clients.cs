using System.ComponentModel.DataAnnotations;

namespace Client.TableNormalization.RentApartment;

public sealed class Clients
{
    /// <summary>
    /// Номер паспорта клиента
    /// </summary>
   [Key] public string PassportId { get; set; }
    
    /// <summary>
    /// ФИО клиента
    /// </summary>
    public string FIO { get; set; }
    
    /// <summary>
    /// Адрес клиента
    /// </summary>
    public string Address { get; set; }
    
    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public string PhoneNumber { get; set; }
}