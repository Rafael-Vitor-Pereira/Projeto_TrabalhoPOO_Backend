using Microsoft.AspNetCore.Mvc;
using Back_end.Dtos.Produto;
using Back_end.Services;
using Back_end.Excecoes;

namespace Back_end.Controllers;

[ApiController]
[Route("produto")]

public class ProdutoController : ControllerBase
{
  private readonly ProdutoServico _produtoServico;

  public ProdutoController([FromServices] ProdutoServico servico)
  {
    _produtoServico = servico;
  }

  [HttpPost]
  public ActionResult<Resposta> Cadastro([FromBody] CriarAtualizarRequisicao novoProduto)
  {

    var resposta = _produtoServico.Cadastrar(novoProduto);

    //Enviar os dados da requisição para a classe de serviço
    return CreatedAtAction(nameof(Buscar), new { id = resposta.Id }, resposta);
  }

  [HttpGet]
  public ActionResult<List<Resposta>> Listar()
  {
    //Pegar e retornar a lista de produtos do serviço
    return Ok(_produtoServico.Listar());
  }

  [HttpGet("{id:int}")]
  public ActionResult<Resposta> Buscar([FromRoute] int id)
  {
    try
    {
      //manda pro serviço buscar o produto pelo id
      return Ok(_produtoServico.Buscar(id));
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
      _produtoServico.Excluir(id);

      return NoContent();
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }

  }

  [HttpPut("{id:int}")]
  public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao produtoEditado)
  {
    try
    {
      //Manda o serviço atualizar o produto
      return Ok(_produtoServico.Atualizar(id, produtoEditado));
    }
    catch (EmailExistente e)
    {
      return BadRequest(new { mensagem = e.Message });
    }
    catch (Exception e)
    {
      return NotFound(new { mensagem = e.Message });
    }
  }

  [HttpPut("{produtoId:int}/ingrediente/{ingredienteId:int}")]
  public ActionResult<Resposta> CadastrarIngrediente([FromRoute] int produtoId, [FromRoute] int ingredienteId)
  {
    try
    {
      return Ok(_produtoServico.AtribuirIngrediente(produtoId, ingredienteId));
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

  [HttpDelete("{produtoId:int}/ingrediente/{ingresienteId:int}")]
  public ActionResult<Resposta> DeleteIngrediente([FromRoute] int produtoId, [FromRoute] int ingredienteId)
  {
    try
    {
      return Ok(_produtoServico.RemoverIngrediente(produtoId, ingredienteId));
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