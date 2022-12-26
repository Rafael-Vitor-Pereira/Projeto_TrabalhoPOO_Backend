using Back_end.Data;
using Back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Repositores;

public class AtendenteRepositorio
{
  private readonly ContextoBD _contexto;

  public AtendenteRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Atendente Cadastrar(Atendente atendente)
  {
    //Manda salvar no banco de dados
    _contexto.Atendentes.Add(atendente);
    _contexto.SaveChanges();

    return atendente;
  }

  public List<Atendente> Listar()
  {
    //retorna uma lista com todos os atendentes cadastrados
    return _contexto.Atendentes.AsNoTracking().ToList();
  }

  public Atendente Buscar(int id, bool tracking = true)
  {
    //Busca o atendente que tem o id recebido por parâmetro
    return (tracking) ? _contexto.Atendentes.FirstOrDefault(atendente => atendente.Id == id) : _contexto.Atendentes.AsNoTracking().FirstOrDefault(atendente => atendente.Id == id);
  }

  public void Atualizar()
  {
    _contexto.SaveChanges();
  }

  public void Excluir(Atendente atendente)
  {
    //mandar o contexto remover
    _contexto.Remove(atendente);
    _contexto.SaveChanges();
  }
}
