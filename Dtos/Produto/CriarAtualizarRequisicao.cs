using System.ComponentModel.DataAnnotations;

namespace TrabalhoPOO.Dtos.Produto;

public class CriarAtualizarRequisicao
{
    [Required(ErrorMessage = "O campo {0} é obrigatório!")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "O {0} deve conter entre {2} e {1} caracteres!")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!")]
    [Range(1, 10_000_000, ErrorMessage = "{0} deve ser entre {1} e {2}!")]
    public decimal? Valor { get; set; }
}
