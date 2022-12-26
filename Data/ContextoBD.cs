using Back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Data;

public class ContextoBD : DbContext
{
  public ContextoBD(DbContextOptions<ContextoBD> options) : base(options)
  {

  }

  //TABELAS
  public DbSet<Cliente> Clientes { get; set; }
  public DbSet<Pedido> Pedidos { get; set; }
  public DbSet<Atendente> Atendentes { get; set; }
  public DbSet<Custo> Custos { get; set; }
  public DbSet<Produto> Produtos { get; set; }
  public DbSet<Ingrediente> Ingredientes { get; set; }
}
