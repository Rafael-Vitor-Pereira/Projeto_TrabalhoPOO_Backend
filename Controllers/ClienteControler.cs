using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Cliente;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("cliente")]

public class ClienteController : ControllerBase{
    private ClienteServico _clienteServico;

	public ClienteController([FromServices] ClienteServico servico){
		_clienteServico = servico;
	}

	[HttpPost]
	public Resposta Cadastro([FromBody] CriarAtualizarRequisicao novoCliente){
		return _clienteServico.CriarCliente(novoCliente);
	}

	[HttpGet]
	public List<Resposta> Listar(){
		return _clienteServico.ListarCliente();
	}

	[HttpGet("{id:int}")]
	public Resposta Buscar([FromRoute] int id){
		return _clienteServico.Buscar(id);
	}

	[HttpDelete("{id:int}")]
	public void Delete([FromRoute] int id){
		_clienteServico.Remover(id);
	}

	[HttpPut("{id:int}")]
	public Resposta Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao clienteEditado){
		return _clienteServico.Atualizar(id, clienteEditado);
	}
}
