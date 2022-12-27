using Back_end.Data;
using Back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Repositores;

public class PedidoRepositorio
{
  private readonly ContextoBD _contexto;

  public PedidoRepositorio([FromServices] ContextoBD contexto)
  {
    _contexto = contexto;
  }

  public Pedido Cadastrar(Pedido pedido)
  {
    //Manda salvar no banco de dados
    _contexto.Pedidos.Add(pedido);
    _contexto.SaveChanges();

    return pedido;
  }

  public List<Pedido> Listar()
  {
    //retorna uma lista com todos os Pedidos cadastrados
    return _contexto.Pedidos.Include(Pedido => Pedido.Atendente).Include(Pedido => Pedido.Cliente)
        .Include(Pedido => Pedido.Produtos).AsNoTracking().ToList();
  }

  public Pedido Buscar(int id, bool tracking = true)
  {
    //Busca o Pedido que tem o id recebido por parâmetro
    return (tracking) ?
        _contexto.Pedidos.Include(Pedido => Pedido.Atendente).Include(Pedido => Pedido.Cliente)
        .Include(Pedido => Pedido.Produtos).FirstOrDefault(pedido => pedido.Id == id) :
        _contexto.Pedidos.AsNoTracking().Include(Pedido => Pedido.Atendente).Include(Pedido => Pedido.Cliente)
        .Include(Pedido => Pedido.Produtos).FirstOrDefault(pedido => pedido.Id == id);
  }

  public void Atualizar()
  {
    _contexto.SaveChanges();
  }

  public void Excluir(Pedido pedido)
  {
    //mandar o contexto remover
    _contexto.Remove(pedido);
    _contexto.SaveChanges();
  }
}
