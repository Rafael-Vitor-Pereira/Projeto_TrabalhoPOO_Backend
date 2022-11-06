using System.ComponentModel.DataAnnotations;

namespace TrabalhoPOO.Dtos.Pedido;

public class CriarAtualizarRequisicao
{
    [Required(ErrorMessage = "O campo {0} é obrigatório!")]
    [Range(1, 10_000_000, ErrorMessage = "{0} deve ser entre {1} e {2}!")]
    public decimal? Valor { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!")]
    public int? AtendenteId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório!")]
    public int? ClienteId { get; set; }

    public DateTime Data { get; set; }
}
