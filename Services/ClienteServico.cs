using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Cliente;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class ClienteServico
{
    private ClienteRepositorio _clienteRepositorio;

	public ClienteServico([FromServices] ClienteRepositorio repositorio){
		_clienteRepositorio = repositorio;
	}

	public Resposta CriarCliente(CriarAtualizarRequisicao novoCliente){
		var cliente = new Cliente();
		RequisicaoParaModelo(novoCliente, cliente);

		var agora = DateTime.Now;
		cliente.DataCadastro = agora;

		_clienteRepositorio.CriarCliente(cliente);

		var resposta = ModeloParaResposta(cliente);

		return resposta;
	}

	public List<Resposta> ListarCliente(){
		var clientes = _clienteRepositorio.ListarCliente();

		List<Resposta> clienteResposta = new();

		foreach (var pessoa in clientes)
		{
			var resposta = ModeloParaResposta(pessoa);

			clienteResposta.Add(resposta);
		}

		return clienteResposta;
	}

	public void Remover(int id){
		var cliente = _clienteRepositorio.Buscar(id);

		if(cliente is null){
			return;
		}

		_clienteRepositorio.Remover(cliente);
	}

	private Resposta ModeloParaResposta(Cliente modelo){
		var resposta = new Resposta();
		resposta.Id = modelo.Id;
		resposta.Nome = modelo.Nome;
		resposta.Email = modelo.Email;
		resposta.Telefone = modelo.Telefone;
		resposta.Usuario = modelo.Usuario;
		resposta.DataCadastro = modelo.DataCadastro;

		return resposta;
	}

	public Resposta Buscar(int id){
		var cliente  = _clienteRepositorio.Buscar(id);

		return ModeloParaResposta(cliente);
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao clienteEditado){
		var cliente = _clienteRepositorio.Buscar(id);

		if(cliente is null){
			return null;
		}

		RequisicaoParaModelo(clienteEditado, cliente);

		_clienteRepositorio.Atualizar();

		return ModeloParaResposta(cliente);
	}

	private void RequisicaoParaModelo(CriarAtualizarRequisicao requisicao, Cliente modelo){
		modelo.Nome = requisicao.Nome;
		modelo.Email = requisicao.Email;
		modelo.Telefone = requisicao.Telefone;
		modelo.Usuario = requisicao.Usuario;
		modelo.Senha = requisicao.Senha;
	}
}
