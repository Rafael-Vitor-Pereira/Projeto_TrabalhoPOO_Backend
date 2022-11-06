using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Pedido;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("pedido")]

public class PedidoController : ControllerBase{
	private readonly PedidoServico _pedidoServico;

	public PedidoController([FromServices] PedidoServico servico){
		_pedidoServico = servico;
	}

	[HttpPost]
	public ActionResult<Resposta> Cadastro([FromBody] CriarAtualizarRequisicao novoPedido){
		var resposta = _pedidoServico.CriarPedido(novoPedido); 
		return CreatedAtAction(nameof(Buscar), new {id = resposta.Id}, resposta);
	}

	[HttpGet]
	public ActionResult<List<Resposta>> Listar(){
		return Ok(_pedidoServico.ListarPedido());
	}

	[HttpGet("{id:int}")]
	public ActionResult<Resposta> Buscar([FromRoute] int id){
		try{
			return Ok(_pedidoServico.Buscar(id));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete([FromRoute] int id){
		try{
			_pedidoServico.Remover(id);

			return NoContent();
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpPut("{id:int}")]
	public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao pedidoEditado){
		try{
			return Ok(_pedidoServico.Atualizar(id, pedidoEditado));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}
}