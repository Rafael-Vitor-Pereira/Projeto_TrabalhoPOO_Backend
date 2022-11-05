namespace TrabalhoPOO.Dtos.Pedido;

public class CriarAtualizarRequisicao
{
    public decimal Valor { get; set; }

    public int AtendenteId { get; set; }

    public int ClienteId { get; set; }

    public DateTime Data { get; set; }
}
