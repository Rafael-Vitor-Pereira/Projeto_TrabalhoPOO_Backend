using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Pedido;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class PedidoServico
{
    private PedidoRepositorio _pedidoRepositorio;

	public PedidoServico([FromServices] PedidoRepositorio repositorio){
		_pedidoRepositorio = repositorio;
	}

	public Resposta CriarPedido(CriarAtualizarRequisicao novoPedido){
		var pedido = new Pedido();
		RequisicaoParaModelo(novoPedido, pedido);

        var agora = DateTime.Now;
		pedido.Data = agora;

		_pedidoRepositorio.CriarPedido(pedido);

		var resposta = ModeloParaResposta(pedido);

		return resposta;
	}

	public List<Resposta> ListarPedido(){
		var pedidos = _pedidoRepositorio.ListarPedido();

		List<Resposta> pedidoResposta = new();

		foreach (var pessoa in pedidos)
		{
			var resposta = ModeloParaResposta(pessoa);

			pedidoResposta.Add(resposta);
		}

		return pedidoResposta;
	}

	public void Remover(int id){
		var pedido = _pedidoRepositorio.Buscar(id);

		if(pedido is null){
			return;
		}

		_pedidoRepositorio.Remover(pedido);
	}

	private Resposta ModeloParaResposta(Pedido modelo){
		var resposta = new Resposta();
		resposta.Id = modelo.Id;
        resposta.Valor = modelo.Valor;
		resposta.AtendenteId = modelo.AtendenteId;
		resposta.ClienteId = modelo.ClienteId;
        resposta.Data = modelo.Data;

		return resposta;
	}

	public Resposta Buscar(int id){
		var pedido  = _pedidoRepositorio.Buscar(id);

		return ModeloParaResposta(pedido);
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao pedidoEditado){
		var pedido = _pedidoRepositorio.Buscar(id);

		if(pedido is null){
			return null;
		}

		RequisicaoParaModelo(pedidoEditado, pedido);

		_pedidoRepositorio.Atualizar();

		return ModeloParaResposta(pedido);
	}

	private void RequisicaoParaModelo(CriarAtualizarRequisicao requisicao, Pedido modelo){
		modelo.Valor = requisicao.Valor;
        modelo.AtendenteId = requisicao.AtendenteId;
        modelo.ClienteId = requisicao.ClienteId;
        modelo.Data = requisicao.Data;
	}
}
