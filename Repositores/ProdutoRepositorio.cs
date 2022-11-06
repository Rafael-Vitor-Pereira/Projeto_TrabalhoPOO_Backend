using TrabalhoPOO.Data;
using TrabalhoPOO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoPOO.Repositores;

public class ProdutoRepositorio{
	private readonly ContextoBD _contexto;

	public ProdutoRepositorio([FromServices] ContextoBD contexto){
		_contexto = contexto;
	}

	public Produto CriarProduto(Produto produto){
		_contexto.Produtos.Add(produto);
		_contexto.SaveChanges();

		return produto;
	}

	public List<Produto> ListarProduto(){
		return _contexto.Produtos.AsNoTracking().ToList();
	}

	public Produto Buscar(int id, bool tracking = true){
		return (tracking) ? _contexto.Produtos.FirstOrDefault(produto => produto.Id == id) : _contexto.Produtos.AsNoTracking().FirstOrDefault(produto => produto.Id == id);	
	}

	public void Remover(Produto produto){
		_contexto.Remove(produto);
		_contexto.SaveChanges();
	}

	public void Atualizar(){
		_contexto.SaveChanges();
	}
}