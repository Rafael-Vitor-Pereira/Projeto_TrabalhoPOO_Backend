using Back_end.Dtos.Custo;
using Back_end.Models;
using Back_end.Repositores;
using Microsoft.AspNetCore.Mvc;
using Mapster;

namespace Back_end.Services;

public class CustoServico
{
  private readonly CustoRepositorio _repositorio;

  public CustoServico([FromServices] CustoRepositorio repositorio)
  {
    _repositorio = repositorio;
  }

  public Resposta Cadastrar(CriarRequisicao novoCusto)
  {
    //copiar os dados da requisição para o modelo
    var custo = novoCusto.Adapt<Custo>();

    //Regras de negócio específica
    var agora = DateTime.Now;
    custo.Data = agora;

    //Enviar para o repositório salvar no BD
    _repositorio.Cadastrar(custo);

    //copiar os dados do modelo para a resposta
    var resposta = custo.Adapt<Resposta>();

    return resposta;
  }

  public List<Resposta> Listar()
  {
    //Buscar todos os custos no repositório
    var custos = _repositorio.Listar();

    //copiando a lista de modelo para a lista de resposta
    var resposta = custos.Adapt<List<Resposta>>();

    //retornar a lista de resposta
    return resposta;
  }

  public Resposta Buscar(int id)
  {
    //Buscar no repositorio pelo id
    var custo = BuscarPeloId(id, false);

    //copiar do modelo para a resposta
    return custo.Adapt<Resposta>();
  }

  public Resposta Atualizar(int id, AtualizarRequisicao custoEditado)
  {
    //Buscar o custo pelo id
    var custo = BuscarPeloId(id);

    //Copiar os dados da requisição para o modelo
    custoEditado.Adapt(custo);

    //Mandar repositório atualizar
    _repositorio.Atualizar();

    //Copiar do modelo para a resposta
    return custo.Adapt<Resposta>();
  }

  public void Excluir(int id)
  {
    //Buscar o custo pelo id
    var custo = BuscarPeloId(id);

    //Mandar o repositorio remover o custo
    _repositorio.Excluir(custo);
  }

  private Custo BuscarPeloId(int id, bool tracking = true)
  {
    var custo = _repositorio.Buscar(id, tracking);

    if (custo is null)
    {
      throw new Exception("Custo não encontrado");
    }

    return custo;
  }
}
