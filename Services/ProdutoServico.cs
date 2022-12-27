using Back_end.Dtos.Produto;
using Back_end.Models;
using Back_end.Repositores;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Back_end.Excecoes;

namespace Back_end.Services;

public class ProdutoServico
{
  private readonly ProdutoRepositorio _repositorio;

  public ProdutoServico([FromServices] ProdutoRepositorio repositorio)
  {
    _repositorio = repositorio;
  }
  public Resposta Cadastrar(CriarRequisicao novoProduto)
  {
    //copiar os dados da requisição para o modelo
    var produto = novoProduto.Adapt<Produto>();

    //Enviar para o repositório salvar no BD
    _repositorio.CriarProduto(produto);

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

  public Resposta Atualizar(int id, AtualizarRequisicao produtoEditado)
  {
    //Buscar o produto pelo id
    var produto = BuscarPeloId(id);

    //se o produto esta alterando seu email
    if (produto.Email != produtoEditado.Email)
    {
      var produtoExistente = _repositorio.BuscarPorEmail(produtoEditado.Email);
      if (produtoExistente is not null)
      {
        throw new EmailExistente();
      }
    }

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
    _repositorio.Excluir(id);
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
}
