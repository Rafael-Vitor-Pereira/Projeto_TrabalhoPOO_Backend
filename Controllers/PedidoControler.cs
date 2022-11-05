using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Pedido;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("pedido")]

public class PedidoController : ControllerBase{
    private PedidoServico _pedidoServico;

	public PedidoController([FromServices] PedidoServico servico){
		_pedidoServico = servico;
	}

	[HttpPost]
	public Resposta Cadastro([FromBody] CriarAtualizarRequisicao novoPedido){
		return _pedidoServico.CriarPedido(novoPedido);
	}

	[HttpGet]
	public List<Resposta> Listar(){
		return _pedidoServico.ListarPedido();
	}

	[HttpGet("{id:int}")]
	public Resposta Buscar([FromRoute] int id){
		return _pedidoServico.Buscar(id);
	}

	[HttpDelete("{id:int}")]
	public void Delete([FromRoute] int id){
		_pedidoServico.Remover(id);
	}

	[HttpPut("{id:int}")]
	public Resposta Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao pedidoEditado){
		return _pedidoServico.Atualizar(id, pedidoEditado);
	}
}
