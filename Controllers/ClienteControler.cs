using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Cliente;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("cliente")]

public class ClienteController : ControllerBase{
	private readonly ClienteServico _clienteServico;

	public ClienteController([FromServices] ClienteServico servico){
		_clienteServico = servico;
	}

	[HttpPost]
	public ActionResult<Resposta> Cadastro([FromBody] CriarAtualizarRequisicao novoCliente){
		var resposta = _clienteServico.CriarCliente(novoCliente); 
		return CreatedAtAction(nameof(Buscar), new {id = resposta.Id}, resposta);
	}

	[HttpGet]
	public ActionResult<List<Resposta>> Listar(){
		return Ok(_clienteServico.ListarCliente());
	}

	[HttpGet("{id:int}")]
	public ActionResult<Resposta> Buscar([FromRoute] int id){
		try{
			return Ok(_clienteServico.Buscar(id));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete([FromRoute] int id){
		try{
			_clienteServico.Remover(id);

			return NoContent();
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpPut("{id:int}")]
	public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao clienteEditado){
		try{
			return Ok(_clienteServico.Atualizar(id, clienteEditado));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}
}