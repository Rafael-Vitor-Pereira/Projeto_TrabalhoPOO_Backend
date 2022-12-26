using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Models;

[Index(nameof(Email), IsUnique = true)]
public class Atendente
{
  [Required]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "varchar(75)")]
  public string Nome { get; set; }

  [Required]
  [Column(TypeName = "varchar(50)")]
  public string Email { get; set; }

  [Required]
  [Column(TypeName = "varchar(20)")]
  public string Telefone { get; set; }

  [Required]
  [Column(TypeName = "varchar(15)")]
  public string Usuario { get; set; }

  [Required]
  [Column(TypeName = "varchar(100)")]
  public string Senha { get; set; }

  [Required]
  public DateTime DataCadastro { get; set; }

  //propriedade de navegação
  public List<Pedido> Pedido { get; set; }
}
