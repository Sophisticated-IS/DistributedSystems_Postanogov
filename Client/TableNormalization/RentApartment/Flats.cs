using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.TableNormalization.RentApartment;

public sealed class Flats
{
    /// <summary>
    /// Код квартиры 
    /// </summary>
   [Key] public uint Id { get; set; }
    
    /// <summary>
    /// Адрес квартиры
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Площадь квартиры
    /// </summary>
    public uint Square { get; set; }
    
    /// <summary>
    /// Этаж
    /// </summary>
    public int Floor  { get; set; }

    /// <summary>
    /// Кол-во комнат
    /// </summary>
    public uint RoomsNumber { get; set; }
    
    
    public FlatCategories FlatCategoryId { get; set; }
}