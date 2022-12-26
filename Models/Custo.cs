using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end.Models;

public class Custo
{
  [Required]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "varchar(30)")]
  public string Tipo { get; set; }

  [Required]
  public int Quant { get; set; }

  [Required]
  [Column(TypeName = "decimal(10,2)")]
  public decimal Valor { get; set; }

  [Required]
  public DateTime Data { get; set; }

  //propriedade de navegação
  public Ingrediente Ingrediente { get; set; }

  //chave estrangeira
  public int IngredienteId { get; set; }
}
