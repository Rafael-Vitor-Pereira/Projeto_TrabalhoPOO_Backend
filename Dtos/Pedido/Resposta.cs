namespace Back_end.Dtos.Pedido;

public class Resposta
{
  public int Id { get; set; }

  public decimal Valor { get; set; }

  public AtendenteResposta Atendente { get; set; }

  public ClienteResposta Cliente { get; set; }

  public List<ProdutoResposta> Produtos { get; set; }

  public DateTime Data { get; set; }
}

public class AtendenteResposta
{
  public int Id { get; set; }

  public string Nome { get; set; }
}

public class ClienteResposta
{
  public int Id { get; set; }

  public string Nome { get; set; }
}

public class ProdutoResposta
{
  public int Id { get; set; }

  public string Nome { get; set; }

  public decimal Valor { get; set; }
}