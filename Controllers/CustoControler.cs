using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Custo;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("custo")]

public class CustoController : ControllerBase{
    private CustoServico _custoServico;

	public CustoController([FromServices] CustoServico servico){
		_custoServico = servico;
	}

	[HttpPost]
	public Resposta Cadastro([FromBody] CriarAtualizarRequisicao novoCusto){
		return _custoServico.CriarCusto(novoCusto);
	}

	[HttpGet]
	public List<Resposta> Listar(){
		return _custoServico.ListarCusto();
	}

	[HttpGet("{id:int}")]
	public Resposta Buscar([FromRoute] int id){
		return _custoServico.Buscar(id);
	}

	[HttpDelete("{id:int}")]
	public void Delete([FromRoute] int id){
		_custoServico.Remover(id);
	}

	[HttpPut("{id:int}")]
	public Resposta Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao custoEditado){
		return _custoServico.Atualizar(id, custoEditado);
	}
}
