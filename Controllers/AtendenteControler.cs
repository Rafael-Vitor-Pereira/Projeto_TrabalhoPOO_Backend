using Microsoft.AspNetCore.Mvc;
using Back_end.Dtos.Atendente;
using Back_end.Services;

namespace Back_end.Controllers;

[ApiController]
[Route("atendente")]

public class AtendenteController : ControllerBase
{
  private readonly AtendenteServico _atendenteServico;

  public AtendenteController([FromServices] AtendenteServico servico)
  {
    _atendenteServico = servico;
  }

  [HttpPost]
  public ActionResult<Resposta> Cadastro([FromBody] CriarRequisicao novoAtendente)
  {
    var resposta = _atendenteServico.Cadastrar(novoAtendente);

    //Enviar os dados da requisição para a classe de serviço
    return CreatedAtAction(nameof(Buscar), new { id = resposta.Id }, resposta);
  }

  [HttpGet]
  public ActionResult<List<Resposta>> Listar()
  {
    //Pegar e retornar a lista de procedimentos do serviço
    return Ok(_atendenteServico.Listar());
  }

  [HttpGet("{id:int}")]
  public ActionResult<Resposta> Buscar([FromRoute] int id)
  {
    try
    {
      //manda pro serviço buscar o atendente pelo id
      return Ok(_atendenteServico.Buscar(id));
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
      _atendenteServico.Excluir(id);

      return NoContent();
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }

  }

  [HttpPut("{id:int}")]
  public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] AtualizarRequisicao atendenteEditado)
  {
    try
    {
      //Manda o serviço atualizar o atendente
      return Ok(_atendenteServico.Atualizar(id, atendenteEditado));
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }

  }
}