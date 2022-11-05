using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoPOO.Models;

public class Ingrediente
{
  [Required]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "varchar(30)")]
  public string Nome { get; set; }

  [Required]
  [Column(TypeName = "decimal(10,2)")]
  public decimal Valor { get; set; }

  //propriedade de navegação
  public Custo Custo { get; set; }

  //propriedade de navegação
  public List<Produto> Produto { get; set; }
}