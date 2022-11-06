using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Atendente;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("atendente")]

public class AtendenteController : ControllerBase{
	private readonly AtendenteServico _atendenteServico;

	public AtendenteController([FromServices] AtendenteServico servico){
		_atendenteServico = servico;
	}

	[HttpPost]
	public ActionResult<Resposta> Cadastro([FromBody] CriarAtualizarRequisicao novoAtendente){
		var resposta = _atendenteServico.CriarAtendente(novoAtendente); 
		return CreatedAtAction(nameof(Buscar), new {id = resposta.Id}, resposta);
	}

	[HttpGet]
	public ActionResult<List<Resposta>> Listar(){
		return Ok(_atendenteServico.ListarAtendente());
	}

	[HttpGet("{id:int}")]
	public ActionResult<Resposta> Buscar([FromRoute] int id){
		try{
			return Ok(_atendenteServico.Buscar(id));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete([FromRoute] int id){
		try{
			_atendenteServico.Remover(id);

			return NoContent();
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpPut("{id:int}")]
	public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao atendenteEditado){
		try{
			return Ok(_atendenteServico.Atualizar(id, atendenteEditado));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}
}