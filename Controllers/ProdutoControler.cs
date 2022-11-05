using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Produto;
using TrabalhoPOO.Services;

namespace TrabalhoPOO.Controllers;

[ApiController]
[Route("produto")]

public class ProdutoController : ControllerBase{
    private ProdutoServico _produtoServico;

	public ProdutoController([FromServices] ProdutoServico servico){
		_produtoServico = servico;
	}

	[HttpPost]
	public Resposta Cadastro([FromBody] CriarAtualizarRequisicao novoProduto){
		return _produtoServico.CriarProduto(novoProduto);
	}

	[HttpGet]
	public List<Resposta> Listar(){
		return _produtoServico.ListarProduto();
	}

	[HttpGet("{id:int}")]
	public Resposta Buscar([FromRoute] int id){
		return _produtoServico.Buscar(id);
	}

	[HttpDelete("{id:int}")]
	public void Delete([FromRoute] int id){
		_produtoServico.Remover(id);
	}

	[HttpPut("{id:int}")]
	public Resposta Editar([FromRoute] int id, [FromBody] CriarAtualizarRequisicao produtoEditado){
		return _produtoServico.Atualizar(id, produtoEditado);
	}
}
