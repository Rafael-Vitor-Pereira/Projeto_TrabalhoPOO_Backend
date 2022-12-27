using Back_end.Data;
using Back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Repositores;

public class IngredienteRepositorio
{
  private readonly ContextoBD _contexto;

  public IngredienteRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Ingrediente Cadastrar(Ingrediente ingrediente)
  {
    //Manda salvar no banco de dados
    _contexto.Ingredientes.Add(ingrediente);
    _contexto.SaveChanges();

    return ingrediente;
  }

  public List<Ingrediente> Listar()
  {
    //retorna uma lista com todos os Ingredientes cadastrados
    return _contexto.Ingredientes.AsNoTracking().ToList();
  }

  public Ingrediente Buscar(int id, bool tracking = true)
  {
    //Busca o ingrediente que tem o id recebido por parâmetro
    return (tracking) ? _contexto.Ingredientes.FirstOrDefault(ingrediente => ingrediente.Id == id) : _contexto.Ingredientes.AsNoTracking().FirstOrDefault(ingrediente => ingrediente.Id == id);
  }

  public void Atualizar()
  {
    _contexto.SaveChanges();
  }

  public void Excluir(Ingrediente ingrediente)
  {
    //mandar o contexto remover
    _contexto.Remove(ingrediente);
    _contexto.SaveChanges();
  }
}
