using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end.Dtos.Atendente;

public class CriarRequisicao
{
  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  [StringLength(80, MinimumLength = 3, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres")]
  public string Nome { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  [StringLength(80)]
  public string Email { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  [StringLength(80, MinimumLength = 10, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres")]
  public string Telefone { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  [StringLength(80, MinimumLength = 3, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres")]
  public string Usuario { get; set; }

  [Required(ErrorMessage = "O campo {0} é obrigatório")]
  [StringLength(80, MinimumLength = 6, ErrorMessage = "{0} deve conter entre {2} e {1} caracteres")]
  public string Senha { get; set; }

  public DateTime DataCadastro { get; set; }
}
