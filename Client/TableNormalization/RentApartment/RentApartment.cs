using System.ComponentModel.DataAnnotations;

namespace Client.TableNormalization.RentApartment;
public sealed class RentApartment
{
    /// <summary>
    /// ID записи договра об аренде
    /// </summary>
   [Key] public uint Id { get; set; }
    
    /// <summary>
    /// № договора об аренде
    /// </summary>
   public Contracts ContractId { get; set; }
    
    /// <summary>
    /// Код квартиры 
    /// </summary>
    public Flats FlatId { get; set; }
    
    /// <summary>
    /// Месяц начала аренды
    /// </summary>
    public byte StartRentMonth { get; set; }
    
    /// <summary>
    /// Год начала аренды
    /// </summary>
    public ushort YearRentMonth { get; set; }
    
    /// <summary>
    /// Кол-во месяцев аренды аренды
    /// </summary>
    public ushort MonthsAmount { get; set; }
    
    /// <summary>
    /// Стоимость аренды в месяц
    /// </summary>
    public uint RentCost { get; set; }
}