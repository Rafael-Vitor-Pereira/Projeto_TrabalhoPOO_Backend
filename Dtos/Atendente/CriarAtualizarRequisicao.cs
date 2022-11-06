using System.ComponentModel.DataAnnotations;

namespace TrabalhoPOO.Dtos.Atendente;

public class CriarAtualizarRequisicao{
	[Required(ErrorMessage = "O campo {0} é obrigatório!")]
	[StringLength(80, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
	public string Nome { get; set; }

	[Required(ErrorMessage = "O campo {0} é obrigatório!")]
	[StringLength(80, MinimumLength = 10, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
	public string Email { get; set; }

	[Required(ErrorMessage = "O campo {0} é obrigatório!")]
	public string Telefone { get; set; }

	[Required(ErrorMessage = "O campo Usuário é obrigatório!")]
	[StringLength(80, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
	public string Usuario { get; set; }

	[Required(ErrorMessage = "O campo {0} é obrigatório!")]
	[StringLength(80, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres")]
  	public string Senha { get; set; }
}