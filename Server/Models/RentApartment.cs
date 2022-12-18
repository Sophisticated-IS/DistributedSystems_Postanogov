using System.ComponentModel.DataAnnotations;

namespace Server.Models;

/// <summary>
/// Ненормализованный объект таблицы "Аренда квартир"
/// </summary>
[Serializable]
public sealed class RentApartment
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
    public string ClientPassportId { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public string FIO { get; set; }

    /// <summary>
    /// Адрес клиента
    /// </summary>
    public string ClientAdress { get; set; }

    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public string ClientPhoneNumber { get; set; }

    /// <summary>
    /// Код квартиры 
    /// </summary>
    public uint FlatId { get; set; }

    /// <summary>
    /// Адрес квартиры
    /// </summary>
    public string FlatAdress { get; set; }

    /// <summary>
    /// Площадь квартиры
    /// </summary>
    public uint FlatSquare { get; set; }

    /// <summary>
    /// Этаж
    /// </summary>
    public int Floor  { get; set; }

    /// <summary>
    /// Кол-во комнат
    /// </summary>
    public uint RoomsNumber { get; set; }

    /// <summary>
    /// Код категории квартиры
    /// </summary>
    public uint FlatCategoryId { get; set; }

    /// <summary>
    /// Категория квартиры
    /// </summary>
    public string FlatCategory { get; set; }

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