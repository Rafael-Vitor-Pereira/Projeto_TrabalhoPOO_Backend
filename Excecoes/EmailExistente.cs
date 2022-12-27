namespace Back_end.Excecoes;

public class EmailExistente : Exception
{
  public EmailExistente() : base("Já existe um atendente com esse E-mail")
  {

  }
}
