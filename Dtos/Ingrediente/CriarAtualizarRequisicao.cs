using System.ComponentModel.DataAnnotations;

namespace Back_end.Dtos.Ingrediente;

public class CriarAtualizarRequisicao
{
  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  [StringLength(80, MinimumLength = 3, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres")]
  public string Nome { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  [Range(0.1, 50, ErrorMessage = "O valor do ingrediente deve ser entre {1} e {2}")]
  public decimal Valor { get; set; }
}
