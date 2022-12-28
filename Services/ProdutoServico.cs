using Back_end.Dtos.Produto;
using Back_end.Models;
using Back_end.Repositores;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace Back_end.Services;

public class ProdutoServico
{
  private readonly ProdutoRepositorio _repositorio;
  private readonly IngredienteRepositorio _repositorioI;

  public ProdutoServico([FromServices] ProdutoRepositorio repositorio, [FromServices] IngredienteRepositorio repositorioI)
  {
    _repositorio = repositorio;
    _repositorioI = repositorioI;
  }
  public Resposta Cadastrar(CriarAtualizarRequisicao novoProduto)
  {
    //copiar os dados da requisição para o modelo
    var produto = novoProduto.Adapt<Produto>();

    //Enviar para o repositório salvar no BD
    _repositorio.Cadastrar(produto);

    //copiar os dados do modelo para a resposta
    var resposta = produto.Adapt<Resposta>();

    return resposta;
  }

  public List<Resposta> Listar()
  {
    //Buscar todos os produtos no repositório
    var produtos = _repositorio.Listar();

    //copiando a lista de modelo para a lista de resposta
    var resposta = produtos.Adapt<List<Resposta>>();

    //retornar a lista de resposta
    return resposta;
  }

  public Resposta Buscar(int id)
  {
    //Buscar no repositorio pelo id
    var produto = BuscarPeloId(id, false);

    //copiar do modelo para a resposta
    return produto.Adapt<Resposta>();
  }

  public Resposta Atualizar(int id, CriarAtualizarRequisicao produtoEditado)
  {
    //Buscar o produto pelo id
    var produto = BuscarPeloId(id);

    //Copiar os dados da requisição para o modelo
    produtoEditado.Adapt(produto);

    //Mandar repositório atualizar
    _repositorio.Atualizar();

    //Copiar do modelo para a resposta
    return produto.Adapt<Resposta>();
  }

  public void Excluir(int id)
  {
    //Buscar o produto pelo id
    var produto = BuscarPeloId(id);

    //Mandar o repositorio remover o produto
    _repositorio.Excluir(produto);
  }

  private Produto BuscarPeloId(int id, bool tracking = true)
  {
    var produto = _repositorio.Buscar(id, tracking);

    if (produto is null)
    {
      throw new Exception("Produto não encontrado");
    }

    return produto;
  }

  public Resposta AtribuirIngrediente(int ProdutoId, int IngredienteId)
  {
    //Buscar no repositorio o produto
    var produto = BuscarPeloId(ProdutoId);

    //Buscar no repositorio o ingrediente
    var ingrediente = _repositorioI.Buscar(IngredienteId);

    if (ingrediente is null)
    {
      throw new Exception("Ingrediente não encontrado");
    }

    //Verificar se o ingrediente já está adicionado pra esse produto
    if (produto.Ingredientes.Exists(ingrediente => ingrediente.Id == IngredienteId))
    {
      throw new BadHttpRequestException("Ingrediente já adicionado anteriormente ao produto");
    }

    //associar o ingrediente para esse produto
    produto.Ingredientes.Add(ingrediente);

    //Mandar o repositorio atualizar o produto
    _repositorio.Atualizar();

    //copiar do modelo para a resposta e retornar
    return produto.Adapt<Resposta>();
  }

  public Resposta RemoverIngrediente(int produtoId, int ingredienteId)
  {
    //Buscar no repositório o produto
    var produto = BuscarPeloId(produtoId);

    //verificar se o ingrediente já não foi removido anteriormente
    if (!produto.Ingredientes.Exists(ingrediente => ingrediente.Id == ingredienteId))
    {
      throw new BadHttpRequestException("Ingrediente já foi removido anteriormente ao produto");
    }

    //remover o ingrediente do produto
    produto.Ingredientes.RemoveAll(ingrediente => ingrediente.Id == ingredienteId);

    //atualizar o produto no BD
    _repositorio.Atualizar();

    //copiar do modelo pra resposta e retornar
    return produto.Adapt<Resposta>();
  }
}
