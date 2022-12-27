namespace Back_end.Dtos.Produto;

public class Resposta
{
  public int Id { get; set; }

  public string Nome { get; set; }

  public decimal Valor { get; set; }

  public List<IngredienteResposta> Ingredientes { get; set; }
}

public class IngredienteResposta
{
  public int Id { get; set; }

  public string Nome { get; set; }

  public decimal Valor { get; set; }
}
