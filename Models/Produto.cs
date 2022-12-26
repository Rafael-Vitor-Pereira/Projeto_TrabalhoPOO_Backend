using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end.Models;

public class Produto
{
  [Required]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "varchar(75)")]
  public string Nome { get; set; }

  [Required]
  [Column(TypeName = "decimal(10,2)")]
  public decimal Valor { get; set; }

  //propriedade de navegação
  public List<Pedido> Pedidos { get; set; }

  //propriedade de navegação
  public List<Ingrediente> Ingredientes { get; set; }
}
