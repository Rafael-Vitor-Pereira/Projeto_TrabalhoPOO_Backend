using Mapster;
using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Cliente;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class ClienteServico{
	private readonly ClienteRepositorio _clienteRepositorio;

	public ClienteServico([FromServices] ClienteRepositorio repositorio){
		_clienteRepositorio = repositorio;
	}

	public Resposta CriarCliente(CriarAtualizarRequisicao novoCliente){
		var cliente = novoCliente.Adapt<Cliente>();

		var agora = DateTime.Now;
		cliente.DataCadastro = agora;

		_clienteRepositorio.CriarCliente(cliente);

		var resposta = cliente.Adapt<Resposta>();

		return resposta;
	}

	public List<Resposta> ListarCliente(){
		var clientes = _clienteRepositorio.ListarCliente();

		var respostas = clientes.Adapt<List<Resposta>>();

		return respostas;
	}

	public void Remover(int id){
		var cliente  = BuscarPeloId(id);

		_clienteRepositorio.Remover(cliente);
	}

	public Resposta Buscar(int id){
		var cliente  = BuscarPeloId(id, false);

		return cliente.Adapt<Resposta>();
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao clienteEditado){
		var cliente  = BuscarPeloId(id);

		clienteEditado.Adapt(cliente);

		_clienteRepositorio.Atualizar();

		return cliente.Adapt<Resposta>();
	}

	private Cliente BuscarPeloId(int id, bool tracking = true){
		var cliente = _clienteRepositorio.Buscar(id, tracking);

		if(cliente is null){
			throw new Exception("Cliente não encontrado!");
		}

		return cliente;
	}
}