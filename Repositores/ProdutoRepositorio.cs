using Back_end.Data;
using Back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Repositores;

public class ProdutoRepositorio
{
  private readonly ContextoBD _contexto;

  public ProdutoRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Produto Cadastrar(Produto produto)
  {
    //Manda salvar no banco de dados
    _contexto.Produtos.Add(produto);
    _contexto.SaveChanges();

    return produto;
  }

  public List<Produto> Listar()
  {
    //retorna uma lista com todos os Produtos cadastrados
    return _contexto.Produtos.AsNoTracking().ToList();
  }

  public Produto Buscar(int id, bool tracking = true)
  {
    //Busca o Produto que tem o id recebido por parâmetro
    return (tracking) ? _contexto.Produtos.FirstOrDefault(produto => produto.Id == id) : _contexto.Produtos.AsNoTracking().FirstOrDefault(produto => produto.Id == id);
  }

  public void Atualizar()
  {
    _contexto.SaveChanges();
  }

  public void Excluir(Produto produto)
  {
    //mandar o contexto remover
    _contexto.Remove(produto);
    _contexto.SaveChanges();
  }
}
