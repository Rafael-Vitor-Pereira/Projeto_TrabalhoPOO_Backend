using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end.Dtos.Pedido;

public class AtualizarRequisicao
{
  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public decimal Valor { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public int AtendenteId { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public int ClienteId { get; set; }
}
