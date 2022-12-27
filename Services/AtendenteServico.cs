using Back_end.Dtos.Atendente;
using Back_end.Models;
using Back_end.Repositores;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Back_end.Excecoes;

namespace Back_end.Services;

public class AtendenteServico
{
  private readonly AtendenteRepositorio _repositorio;

  public AtendenteServico([FromServices] AtendenteRepositorio repositorio)
  {
    _repositorio = repositorio;
  }
  public Resposta Cadastrar(CriarRequisicao novoAtendente)
  {
    //verificando se existe atendente com o mesmo email
    var atendenteExistente = _repositorio.BuscarPorEmail(novoAtendente.Email);

    if (atendenteExistente is not null)
    {
      throw new EmailExistente();
    }

    //copiar os dados da requisição para o modelo
    var atendente = novoAtendente.Adapt<Atendente>();

    //Regras de negócio específica
    var agora = DateTime.Now;
    atendente.DataCadastro = agora;

    //Criptografando a senha
    atendente.Senha = BCrypt.Net.BCrypt.HashPassword(atendente.Senha);

    //Enviar para o repositório salvar no BD
    _repositorio.Cadastrar(atendente);

    //copiar os dados do modelo para a resposta
    var resposta = atendente.Adapt<Resposta>();

    return resposta;
  }

  public List<Resposta> Listar()
  {
    //Buscar todos os atendentes no repositório
    var atendentes = _repositorio.Listar();

    //copiando a lista de modelo para a lista de resposta
    var resposta = atendentes.Adapt<List<Resposta>>();

    //retornar a lista de resposta
    return resposta;
  }

  public Resposta Buscar(int id)
  {
    //Buscar no repositorio pelo id
    var atendente = BuscarPeloId(id, false);

    //copiar do modelo para a resposta
    return atendente.Adapt<Resposta>();
  }

  public Resposta Atualizar(int id, AtualizarRequisicao atendenteEditado)
  {
    //Buscar o atendente pelo id
    var atendente = BuscarPeloId(id);

    //se o atendente esta alterando seu email
    if (atendente.Email != atendenteEditado.Email)
    {
      var atendenteExistente = _repositorio.BuscarPorEmail(atendenteEditado.Email);
      if (atendenteExistente is not null)
      {
        throw new EmailExistente();
      }
    }

    //Copiar os dados da requisição para o modelo
    atendenteEditado.Adapt(atendente);

    //Mandar repositório atualizar
    _repositorio.Atualizar();

    //Copiar do modelo para a resposta
    return atendente.Adapt<Resposta>();
  }

  public void Excluir(int id)
  {
    //Buscar o atendente pelo id
    var atendente = BuscarPeloId(id);

    //Mandar o repositorio remover o atendente
    _repositorio.Excluir(atendente);
  }

  private Atendente BuscarPeloId(int id, bool tracking = true)
  {
    var atendente = _repositorio.Buscar(id, tracking);

    if (atendente is null)
    {
      throw new Exception("Atendente não encontrado");
    }

    return atendente;
  }
}
