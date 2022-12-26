using Microsoft.AspNetCore.Mvc;
using Back_end.Dtos.Cliente;
using Back_end.Services;

namespace Back_end.Controllers;

[ApiController]
[Route("cliente")]

public class ClienteController : ControllerBase
{
  private readonly ClienteServico _clienteServico;

  public ClienteController([FromServices] ClienteServico servico)
  {
    _clienteServico = servico;
  }

  [HttpPost]
  public ActionResult<Resposta> Cadastro([FromBody] CriarRequisicao novoCliente)
  {
    var resposta = _clienteServico.Cadastrar(novoCliente);

    //Enviar os dados da requisição para a classe de serviço
    return CreatedAtAction(nameof(Buscar), new { id = resposta.Id }, resposta);
  }

  [HttpGet]
  public ActionResult<List<Resposta>> Listar()
  {
    //Pegar e retornar a lista de clientes do serviço
    return Ok(_clienteServico.Listar());
  }

  [HttpGet("{id:int}")]
  public ActionResult<Resposta> Buscar([FromRoute] int id)
  {
    try
    {
      //manda pro serviço buscar o cliente pelo id
      return Ok(_clienteServico.Buscar(id));
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
      _clienteServico.Excluir(id);

      return NoContent();
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }

  }

  [HttpPut("{id:int}")]
  public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] AtualizarRequisicao clienteEditado)
  {
    try
    {
      //Manda o serviço atualizar o cliente
      return Ok(_clienteServico.Atualizar(id, clienteEditado));
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }

  }
}