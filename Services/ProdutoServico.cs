using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Produto;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class ProdutoServico
{
    private ProdutoRepositorio _proProdutoRepositorio;

	public ProdutoServico([FromServices] ProdutoRepositorio repositorio){
		_proProdutoRepositorio = repositorio;
	}

	public Resposta CriarProduto(CriarAtualizarRequisicao novoProduto){
		var proProduto = new Produto();
		RequisicaoParaModelo(novoProduto, proProduto);

		_proProdutoRepositorio.CriarProduto(proProduto);

		var resposta = ModeloParaResposta(proProduto);

		return resposta;
	}

	public List<Resposta> ListarProduto(){
		var proProdutos = _proProdutoRepositorio.ListarProduto();

		List<Resposta> proProdutoResposta = new();

		foreach (var pessoa in proProdutos)
		{
			var resposta = ModeloParaResposta(pessoa);

			proProdutoResposta.Add(resposta);
		}

		return proProdutoResposta;
	}

	public void Remover(int id){
		var proProduto = _proProdutoRepositorio.Buscar(id);

		if(proProduto is null){
			return;
		}

		_proProdutoRepositorio.Remover(proProduto);
	}

	private Resposta ModeloParaResposta(Produto modelo){
		var resposta = new Resposta();
		resposta.Id = modelo.Id;
        resposta.Nome = modelo.Nome;
		resposta.Valor = modelo.Valor;

		return resposta;
	}

	public Resposta Buscar(int id){
		var proProduto  = _proProdutoRepositorio.Buscar(id);

		return ModeloParaResposta(proProduto);
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao proProdutoEditado){
		var proProduto = _proProdutoRepositorio.Buscar(id);

		if(proProduto is null){
			return null;
		}

		RequisicaoParaModelo(proProdutoEditado, proProduto);

		_proProdutoRepositorio.Atualizar();

		return ModeloParaResposta(proProduto);
	}

	private void RequisicaoParaModelo(CriarAtualizarRequisicao requisicao, Produto modelo){
		modelo.Nome = requisicao.Nome;
        modelo.Valor = requisicao.Valor;
	}
}
