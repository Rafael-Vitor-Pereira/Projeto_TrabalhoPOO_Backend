using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Ingrediente;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("ingrediente")]

public class IngredienteController : ControllerBase{
    private IngredienteServico _ingredienteServico;

	public IngredienteController([FromServices] IngredienteServico servico){
		_ingredienteServico = servico;
	}

	[HttpPost]
	public Resposta Cadastro([FromBody] CriarAtualizarRequisicao novoIngrediente){
		return _ingredienteServico.CriarIngrediente(novoIngrediente);
	}

	[HttpGet]
	public List<Resposta> Listar(){
		return _ingredienteServico.ListarIngrediente();
	}

	[HttpGet("{id:int}")]
	public Resposta Buscar([FromRoute] int id){
		return _ingredienteServico.Buscar(id);
	}

	[HttpDelete("{id:int}")]
	public void Delete([FromRoute] int id){
		_ingredienteServico.Remover(id);
	}

	[HttpPut("{id:int}")]
	public Resposta Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao ingredienteEditado){
		return _ingredienteServico.Atualizar(id, ingredienteEditado);
	}
}
