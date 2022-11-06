using Mapster;
using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Produto;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class ProdutoServico{
	private readonly ProdutoRepositorio _produtoRepositorio;

	public ProdutoServico([FromServices] ProdutoRepositorio repositorio){
		_produtoRepositorio = repositorio;
	}

	public Resposta CriarProduto(CriarAtualizarRequisicao novoProduto){
		var produto = novoProduto.Adapt<Produto>();

		_produtoRepositorio.CriarProduto(produto);

		var resposta = produto.Adapt<Resposta>();

		return resposta;
	}

	public List<Resposta> ListarProduto(){
		var produtos = _produtoRepositorio.ListarProduto();

		var respostas = produtos.Adapt<List<Resposta>>();

		return respostas;
	}

	public void Remover(int id){
		var produto  = BuscarPeloId(id);

		_produtoRepositorio.Remover(produto);
	}

	public Resposta Buscar(int id){
		var produto  = BuscarPeloId(id, false);

		return produto.Adapt<Resposta>();
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao produtoEditado){
		var produto  = BuscarPeloId(id);

		produtoEditado.Adapt(produto);

		_produtoRepositorio.Atualizar();

		return produto.Adapt<Resposta>();
	}

	private Produto BuscarPeloId(int id, bool tracking = true){
		var produto = _produtoRepositorio.Buscar(id, tracking);

		if(produto is null){
			throw new Exception("produto não encontrado!");
		}

		return produto;
	}
}