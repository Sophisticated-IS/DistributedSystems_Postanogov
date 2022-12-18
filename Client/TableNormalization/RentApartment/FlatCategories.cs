using System.ComponentModel.DataAnnotations;

namespace Client.TableNormalization.RentApartment;

public sealed class FlatCategories
{
    /// <summary>
    /// Код категории квартиры
    /// </summary>
  [Key]  public uint FlatCategoryId { get; set; }

    /// <summary>
    /// Категория квартиры
    /// </summary>
    public string FlatCategory { get; set; }
    
}