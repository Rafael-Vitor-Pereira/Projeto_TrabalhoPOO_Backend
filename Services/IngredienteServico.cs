using Back_end.Dtos.Ingrediente;
using Back_end.Models;
using Back_end.Repositores;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Back_end.Excecoes;

namespace Back_end.Services;

public class IngredienteServico
{
  private readonly IngredienteRepositorio _repositorio;

  public IngredienteServico([FromServices] IngredienteRepositorio repositorio)
  {
    _repositorio = repositorio;
  }
  public Resposta Cadastrar(CriarRequisicao novoIngrediente)
  {
    //copiar os dados da requisição para o modelo
    var ingrediente = novoIngrediente.Adapt<Ingrediente>();

    //Enviar para o repositório salvar no BD
    _repositorio.CriarIngrediente(ingrediente);

    //copiar os dados do modelo para a resposta
    var resposta = ingrediente.Adapt<Resposta>();

    return resposta;
  }

  public List<Resposta> Listar()
  {
    //Buscar todos os ingredientes no repositório
    var ingredientes = _repositorio.Listar();

    //copiando a lista de modelo para a lista de resposta
    var resposta = ingredientes.Adapt<List<Resposta>>();

    //retornar a lista de resposta
    return resposta;
  }

  public Resposta Buscar(int id)
  {
    //Buscar no repositorio pelo id
    var ingrediente = BuscarPeloId(id, false);

    //copiar do modelo para a resposta
    return ingrediente.Adapt<Resposta>();
  }

  public Resposta Atualizar(int id, AtualizarRequisicao ingredienteEditado)
  {
    //Buscar o ingrediente pelo id
    var ingrediente = BuscarPeloId(id);

    //se o ingrediente esta alterando seu email
    if (ingrediente.Email != ingredienteEditado.Email)
    {
      var ingredienteExistente = _repositorio.BuscarPorEmail(ingredienteEditado.Email);
      if (ingredienteExistente is not null)
      {
        throw new EmailExistente();
      }
    }

    //Copiar os dados da requisição para o modelo
    ingredienteEditado.Adapt(ingrediente);

    //Mandar repositório atualizar
    _repositorio.Atualizar();

    //Copiar do modelo para a resposta
    return ingrediente.Adapt<Resposta>();
  }

  public void Excluir(int id)
  {
    //Buscar o ingrediente pelo id
    var ingrediente = BuscarPeloId(id);

    //Mandar o repositorio remover o ingrediente
    _repositorio.Excluir(id);
  }

  private Ingrediente BuscarPeloId(int id, bool tracking = true)
  {
    var ingrediente = _repositorio.Buscar(id, tracking);

    if (ingrediente is null)
    {
      throw new Exception("Ingrediente não encontrado");
    }

    return ingrediente;
  }
}
