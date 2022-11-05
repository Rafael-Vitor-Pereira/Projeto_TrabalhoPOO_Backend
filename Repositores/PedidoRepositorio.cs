using TrabalhoPOO.Data;
using TrabalhoPOO.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrabalhoPOO.Repositores;

public class PedidoRepositorio{
    private ContextoBD _contexto;

	public PedidoRepositorio([FromServices] ContextoBD contexto){
		_contexto = contexto;
	}

	public Pedido CriarPedido(Pedido pedido){
		_contexto.Pedidos.Add(pedido);
		_contexto.SaveChanges();

		return pedido;
	}

	public List<Pedido> ListarPedido(){
		return _contexto.Pedidos.ToList();
	}

	public Pedido Buscar(int id){
		return _contexto.Pedidos.FirstOrDefault(pedido => pedido.Id == id);
	}

	public void Remover(Pedido pedido){
		_contexto.Remove(pedido);
		_contexto.SaveChanges();
	}

	public void Atualizar(){
		_contexto.SaveChanges();
	}
}
