namespace Back_end.Dtos.Custo;

public class Resposta
{
  public int Id { get; set; }

  public string Tipo { get; set; }

  public int Quant { get; set; }

  public decimal Valor { get; set; }

  public DateTime Data { get; set; }

  public IngredienteResposta Ingrediente { get; set; }
}

public class IngredienteResposta
{
  public int Id { get; set; }

  public string Nome { get; set; }

  public decimal Valor { get; set; }
}
