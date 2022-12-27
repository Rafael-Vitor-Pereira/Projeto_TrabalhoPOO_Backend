using Microsoft.AspNetCore.Mvc;
using Back_end.Dtos.Ingrediente;
using Back_end.Services;

namespace Back_end.Controllers;

[ApiController]
[Route("ingrediente")]

public class IngredienteController : ControllerBase
{
  private readonly IngredienteServico _ingredienteServico;

  public IngredienteController([FromServices] IngredienteServico servico)
  {
    _ingredienteServico = servico;
  }

  [HttpPost]
  public ActionResult<Resposta> Cadastro([FromBody] CriarAtualizarRequisicao novoIngrediente)
  {
    var resposta = _ingredienteServico.Cadastrar(novoIngrediente);

    //Enviar os dados da requisição para a classe de serviço
    return CreatedAtAction(nameof(Buscar), new { id = resposta.Id }, resposta);
  }

  [HttpGet]
  public ActionResult<List<Resposta>> Listar()
  {
    //Pegar e retornar a lista de ingredientes do serviço
    return Ok(_ingredienteServico.Listar());
  }

  [HttpGet("{id:int}")]
  public ActionResult<Resposta> Buscar([FromRoute] int id)
  {
    try
    {
      //manda pro serviço buscar o ingrediente pelo id
      return Ok(_ingredienteServico.Buscar(id));
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }

  }

  [HttpDelete("{id:int}")]
  public ActionResult Delete([FromRoute] int id)
  {
    try
    {
      //Manda o serviço remover
      _ingredienteServico.Excluir(id);

      return NoContent();
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }

  }

  [HttpPut("{id:int}")]
  public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao ingredienteEditado)
  {
    try
    {
      //Manda o serviço atualizar o ingrediente
      return Ok(_ingredienteServico.Atualizar(id, ingredienteEditado));
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }

  }
}