using Back_end.Data;
using Back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Repositores;

public class ClienteRepositorio
{
  private readonly ContextoBD _contexto;

  public ClienteRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Cliente Cadastrar(Cliente cliente)
  {
    //Manda salvar no banco de dados
    _contexto.Clientes.Add(cliente);
    _contexto.SaveChanges();

    return cliente;
  }

  public List<Cliente> Listar()
  {
    //retorna uma lista com todos os cliente cadastrados
    return _contexto.Clientes.AsNoTracking().ToList();
  }

  public Cliente Buscar(int id, bool tracking = true)
  {
    //Busca o cliente que tem o id recebido por parâmetro
    return (tracking) ? _contexto.Clientes.FirstOrDefault(cliente => cliente.Id == id) : _contexto.Clientes.AsNoTracking().FirstOrDefault(cliente => cliente.Id == id);
  }

  public void Atualizar()
  {
    _contexto.SaveChanges();
  }

  public void Excluir(Cliente cliente)
  {
    //mandar o contexto remover
    _contexto.Remove(cliente);
    _contexto.SaveChanges();
  }
}
