using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Ingrediente;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("ingrediente")]

public class IngredienteController : ControllerBase{
	private readonly IngredienteServico _ingredienteServico;

	public IngredienteController([FromServices] IngredienteServico servico){
		_ingredienteServico = servico;
	}

	[HttpPost]
	public ActionResult<Resposta> Cadastro([FromBody] CriarAtualizarRequisicao novoIngrediente){
		var resposta = _ingredienteServico.CriarIngrediente(novoIngrediente); 
		return CreatedAtAction(nameof(Buscar), new {id = resposta.Id}, resposta);
	}

	[HttpGet]
	public ActionResult<List<Resposta>> Listar(){
		return Ok(_ingredienteServico.ListarIngrediente());
	}

	[HttpGet("{id:int}")]
	public ActionResult<Resposta> Buscar([FromRoute] int id){
		try{
			return Ok(_ingredienteServico.Buscar(id));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpDelete("{id:int}")]
	public ActionResult Delete([FromRoute] int id){
		try{
			_ingredienteServico.Remover(id);

			return NoContent();
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}

	[HttpPut("{id:int}")]
	public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao ingredienteEditado){
		try{
			return Ok(_ingredienteServico.Atualizar(id, ingredienteEditado));
		}catch(Exception e){
			return NotFound(new {mensagem = e.Message});
		}
		
	}
}