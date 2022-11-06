using Mapster;
using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Pedido;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class PedidoServico{
	private readonly PedidoRepositorio _pedidoRepositorio;

	public PedidoServico([FromServices] PedidoRepositorio repositorio){
		_pedidoRepositorio = repositorio;
	}

	public Resposta CriarPedido(CriarAtualizarRequisicao novoPedido){
		var pedido = novoPedido.Adapt<Pedido>();

		var agora = DateTime.Now;
		pedido.Data = agora;

		_pedidoRepositorio.CriarPedido(pedido);

		var resposta = pedido.Adapt<Resposta>();

		return resposta;
	}

	public List<Resposta> ListarPedido(){
		var pedidos = _pedidoRepositorio.ListarPedido();

		var respostas = pedidos.Adapt<List<Resposta>>();

		return respostas;
	}

	public void Remover(int id){
		var pedido  = BuscarPeloId(id);

		_pedidoRepositorio.Remover(pedido);
	}

	public Resposta Buscar(int id){
		var pedido  = BuscarPeloId(id, false);

		return pedido.Adapt<Resposta>();
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao pedidoEditado){
		var pedido  = BuscarPeloId(id);

		pedidoEditado.Adapt(pedido);

		_pedidoRepositorio.Atualizar();

		return pedido.Adapt<Resposta>();
	}

	private Pedido BuscarPeloId(int id, bool tracking = true){
		var pedido = _pedidoRepositorio.Buscar(id, tracking);

		if(pedido is null){
			throw new Exception("pedido não encontrado!");
		}

		return pedido;
	}
}