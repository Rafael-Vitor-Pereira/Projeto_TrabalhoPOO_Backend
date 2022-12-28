using System.ComponentModel.DataAnnotations;

namespace Back_end.Dtos.Custo;

public class CriarRequisicao
{
  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  [StringLength(20)]
  public string Tipo { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public int Quant { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public decimal Valor { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  public DateTime Data { get; set; }

  public int? IngredienteId { get; set; }
}
