using Back_end.Data;
using Back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Repositores;

public class CustoRepositorio
{
  private readonly ContextoBD _contexto;

  public CustoRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Custo Cadastrar(Custo custo)
  {
    //Manda salvar no banco de dados
    _contexto.Custos.Add(custo);
    _contexto.SaveChanges();

    return custo;
  }

  public List<Custo> Listar()
  {
    //retorna uma lista com todos os Custos cadastrados
    return _contexto.Custos.AsNoTracking().Include(custo => custo.Ingrediente).ToList();
  }

  public Custo Buscar(int id, bool tracking = true)
  {
    //Busca o custo que tem o id recebido por parâmetro
    return (tracking) ? _contexto.Custos.FirstOrDefault(custo => custo.Id == id) : _contexto.Custos.AsNoTracking().FirstOrDefault(custo => custo.Id == id);
  }

  public void Atualizar()
  {
    _contexto.SaveChanges();
  }

  public void Excluir(Custo custo)
  {
    //mandar o contexto remover
    _contexto.Remove(custo);
    _contexto.SaveChanges();
  }
}
