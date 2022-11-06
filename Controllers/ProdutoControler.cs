using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Produto;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("produto")]

public class ProdutoController : ControllerBase{
	private readonly ProdutoServico _produtoServico;

	public ProdutoController([FromServices] ProdutoServico servico){
		_produtoServico = servico;
	}

	[HttpPost]
	public ActionResult<Resposta> Cadastro([FromBody] CriarAtualizarRequisicao novoProduto){
		var resposta = _produtoServico.CriarProduto(novoProduto); 
		return CreatedAtAction(nameof(Buscar), new {id = resposta.Id}, resposta);
	}

	[HttpGet]
	public ActionResult<List<Resposta>> Listar(){
		return Ok(_produtoServico.ListarProduto());
	}

	[HttpGet("{id:int}")]
	public ActionResult<Resposta> Buscar([FromRoute] int id){
		try{
			return Ok(_produtoServico.Buscar(id));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete([FromRoute] int id){
		try{
			_produtoServico.Remover(id);

			return NoContent();
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpPut("{id:int}")]
	public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao produtoEditado){
		try{
			return Ok(_produtoServico.Atualizar(id, produtoEditado));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}
}