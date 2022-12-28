using System.ComponentModel.DataAnnotations;

namespace Back_end.Dtos.Pedido;

public class CriarRequisicao
{
  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public decimal Valor { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public int AtendenteId { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public int ClienteId { get; set; }

  public DateTime Data { get; set; }
}
