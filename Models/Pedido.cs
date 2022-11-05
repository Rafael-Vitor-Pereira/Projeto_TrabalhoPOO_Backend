using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabalhoPOO.Models;

public class Pedido
{
  [Required]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "decimal(10,2)")]
  public decimal Valor { get; set; }

  //propriedade de navegação
  public Atendente Atendente { get; set; }

  //Chave Estrangeira
  public int AtendenteId { get; set; }

  //propriedade de navegação
  public Cliente Cliente { get; set; }

  //Chave Estrangeira
  public int ClienteId { get; set; }

  [Required]
  public DateTime Data { get; set; }

  //propriedade de navegação
  public List<Produto> Produtos { get; set; }
}
