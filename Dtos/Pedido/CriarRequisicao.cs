using System.ComponentModel.DataAnnotations;

namespace Back_end.Dtos.Pedido;

public class CriarRequisicao
{
  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  [Range(0.1, 50, ErrorMessage = "O valor do ingrediente deve ser entre {1} e {2}")]
  public decimal Valor { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public int AtendenteId { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public int ClienteId { get; set; }

  public DateTime Data { get; set; }
}
