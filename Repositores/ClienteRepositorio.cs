using TrabalhoPOO.Data;
using TrabalhoPOO.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrabalhoPOO.Repositores;

public class ClienteRepositorio{
    private ContextoBD _contexto;

	public ClienteRepositorio([FromServices] ContextoBD contexto){
		_contexto = contexto;
	}

	public Cliente CriarCliente(Cliente cliente){
		_contexto.Clientes.Add(cliente);
		_contexto.SaveChanges();

		return cliente;
	}

	public List<Cliente> ListarCliente(){
		return _contexto.Clientes.ToList();
	}

	public Cliente Buscar(int id){
		return _contexto.Clientes.FirstOrDefault(cliente => cliente.Id == id);
	}

	public void Remover(Cliente cliente){
		_contexto.Remove(cliente);
		_contexto.SaveChanges();
	}

	public void Atualizar(){
		_contexto.SaveChanges();
	}
}
