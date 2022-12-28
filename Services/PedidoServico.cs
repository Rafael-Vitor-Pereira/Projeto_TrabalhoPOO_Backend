using Back_end.Dtos.Pedido;
using Back_end.Models;
using Back_end.Repositores;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace Back_end.Services;

public class PedidoServico
{
  private readonly PedidoRepositorio _repositorio;
  private readonly ProdutoRepositorio _repositorioP;

  public PedidoServico([FromServices] PedidoRepositorio repositorio, [FromServices] ProdutoRepositorio repositorioP)
  {
    _repositorio = repositorio;
    _repositorioP = repositorioP;
  }
  public Resposta Cadastrar(CriarRequisicao novoPedido)
  {
    //copiar os dados da requisição para o modelo
    var pedido = novoPedido.Adapt<Pedido>();

    //Regras de negócio específica
    var agora = DateTime.Now;
    pedido.Data = agora;

    //Enviar para o repositório salvar no BD
    _repositorio.Cadastrar(pedido);

    //copiar os dados do modelo para a resposta
    var resposta = pedido.Adapt<Resposta>();

    return resposta;
  }

  public List<Resposta> Listar()
  {
    //Buscar todos os pedidos no repositório
    var pedidos = _repositorio.Listar();

    //copiando a lista de modelo para a lista de resposta
    var resposta = pedidos.Adapt<List<Resposta>>();

    //retornar a lista de resposta
    return resposta;
  }

  public Resposta Buscar(int id)
  {
    //Buscar no repositorio pelo id
    var pedido = BuscarPeloId(id, false);

    //copiar do modelo para a resposta
    return pedido.Adapt<Resposta>();
  }

  public Resposta Atualizar(int id, AtualizarRequisicao pedidoEditado)
  {
    //Buscar o pedido pelo id
    var pedido = BuscarPeloId(id);

    //Copiar os dados da requisição para o modelo
    pedidoEditado.Adapt(pedido);

    //Mandar repositório atualizar
    _repositorio.Atualizar();

    //Copiar do modelo para a resposta
    return pedido.Adapt<Resposta>();
  }

  public void Excluir(int id)
  {
    //Buscar o pedido pelo id
    var pedido = BuscarPeloId(id);

    //Mandar o repositorio remover o pedido
    _repositorio.Excluir(pedido);
  }

  private Pedido BuscarPeloId(int id, bool tracking = true)
  {
    var pedido = _repositorio.Buscar(id, tracking);

    if (pedido is null)
    {
      throw new Exception("Pedido não encontrado");
    }

    return pedido;
  }

  public Resposta AtribuirProduto(int PedidoId, int ProdutoId)
  {
    //Buscar no repositorio o pedido
    var pedido = BuscarPeloId(PedidoId);

    //Buscar no repositorio o produto
    var produto = _repositorioP.Buscar(ProdutoId);

    if (produto is null)
    {
      throw new Exception("Produto não encontrado");
    }

    //Verificar se o produto já está adicionado pra esse produto
    if (pedido.Produtos.Exists(produto => produto.Id == ProdutoId))
    {
      throw new BadHttpRequestException("Produto já adicionado anteriormente ao produto");
    }

    //associar o produto para esse pedido
    pedido.Produtos.Add(produto);

    //Mandar o repositorio atualizar o pedido
    _repositorio.Atualizar();

    //copiar do modelo para a resposta e retornar
    return pedido.Adapt<Resposta>();
  }

  public Resposta RemoverProduto(int PedidoId, int produtoId)
  {
    //Buscar no repositório o pedido
    var pedido = BuscarPeloId(PedidoId);

    //verificar se o produto já não foi removido anteriormente
    if (!pedido.Produtos.Exists(produto => produto.Id == produtoId))
    {
      throw new BadHttpRequestException("Produto já foi removido anteriormente do pedido");
    }

    //remover o ingrediente do produto
    pedido.Produtos.RemoveAll(produto => produto.Id == produtoId);

    //atualizar o produto no BD
    _repositorio.Atualizar();

    //copiar do modelo pra resposta e retornar
    return pedido.Adapt<Resposta>();
  }
}
