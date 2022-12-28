using Microsoft.AspNetCore.Mvc;
using Back_end.Dtos.Pedido;
using Back_end.Services;

namespace Back_end.Controllers;

[ApiController]
[Route("pedido")]

public class PedidoController : ControllerBase
{
  private readonly PedidoServico _pedidoServico;

  public PedidoController([FromServices] PedidoServico servico)
  {
    _pedidoServico = servico;
  }

  [HttpPost]
  public ActionResult<Resposta> Cadastro([FromBody] CriarRequisicao novoPedido)
  {

    var resposta = _pedidoServico.Cadastrar(novoPedido);

    //Enviar os dados da requisição para a classe de serviço
    return CreatedAtAction(nameof(Buscar), new { id = resposta.Id }, resposta);
  }

  [HttpGet]
  public ActionResult<List<Resposta>> Listar()
  {
    //Pegar e retornar a lista de pedidos do serviço
    return Ok(_pedidoServico.Listar());
  }

  [HttpGet("{id:int}")]
  public ActionResult<Resposta> Buscar([FromRoute] int id)
  {
    try
    {
      //manda pro serviço buscar o pedido pelo id
      return Ok(_pedidoServico.Buscar(id));
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
      _pedidoServico.Excluir(id);

      return NoContent();
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }

  }

  [HttpPut("{id:int}")]
  public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] AtualizarRequisicao pedidoEditado)
  {
    try
    {
      //Manda o serviço atualizar o pedido
      return Ok(_pedidoServico.Atualizar(id, pedidoEditado));
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }
  }

  [HttpPut("{pedidoId:int}/produto/{produtoId:int}")]
  public ActionResult<Resposta> CadastrarProduto([FromRoute] int pedidoId, [FromRoute] int produtoId)
  {
    try
    {
      return Ok(_pedidoServico.AtribuirProduto(pedidoId, produtoId));
    }
    catch (BadHttpRequestException e)
    {
      return BadRequest(new { mensagem = e.Message });
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }
  }

  [HttpDelete("{pedidoId:int}/produto/{produtoId:int}")]
  public ActionResult<Resposta> DeleteProduto([FromRoute] int pedidoId, [FromRoute] int produtoId)
  {
    try
    {
      return Ok(_pedidoServico.RemoverProduto(pedidoId, produtoId));
    }
    catch (BadHttpRequestException e)
    {
      return BadRequest(new { mensagem = e.Message });
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }
  }
}