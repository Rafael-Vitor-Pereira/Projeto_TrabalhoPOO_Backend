using Back_end.Dtos.Cliente;
using Back_end.Models;
using Back_end.Repositores;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Back_end.Excecoes;

namespace Back_end.Services;

public class ClienteServico
{
  private readonly ClienteRepositorio _repositorio;

  public ClienteServico([FromServices] ClienteRepositorio repositorio)
  {
    _repositorio = repositorio;
  }
  public Resposta Cadastrar(CriarRequisicao novoCliente)
  {
    //verificando se existe cliente com o mesmo email
    var clienteExistente = _repositorio.BuscarPorEmail(novoCliente.Email);

    if (clienteExistente is not null)
    {
      throw new EmailExistente();
    }

    //copiar os dados da requisição para o modelo
    var cliente = novoCliente.Adapt<Cliente>();

    //Regras de negócio específica
    var agora = DateTime.Now;
    cliente.DataCadastro = agora;

    //Criptografando a senha
    cliente.Senha = BCrypt.Net.BCrypt.HashPassword(cliente.Senha);

    //Enviar para o repositório salvar no BD
    _repositorio.Cadastrar(cliente);

    //copiar os dados do modelo para a resposta
    var resposta = cliente.Adapt<Resposta>();

    return resposta;
  }

  public List<Resposta> Listar()
  {
    //Buscar todos os clientes no repositório
    var clientes = _repositorio.Listar();

    //copiando a lista de modelo para a lista de resposta
    var resposta = clientes.Adapt<List<Resposta>>();

    //retornar a lista de resposta
    return resposta;
  }

  public Resposta Buscar(int id)
  {
    //Buscar no repositorio pelo id
    var cliente = BuscarPeloId(id, false);

    //copiar do modelo para a resposta
    return cliente.Adapt<Resposta>();
  }

  public Resposta Atualizar(int id, AtualizarRequisicao clienteEditado)
  {
    //Buscar o cliente pelo id
    var cliente = BuscarPeloId(id);

    //se o cliente esta alterando seu email
    if (cliente.Email != clienteEditado.Email)
    {
      var clienteExistente = _repositorio.BuscarPorEmail(clienteEditado.Email);
      if (clienteExistente is not null)
      {
        throw new EmailExistente();
      }
    }

    //Copiar os dados da requisição para o modelo
    clienteEditado.Adapt(cliente);

    //Mandar repositório atualizar
    _repositorio.Atualizar();

    //Copiar do modelo para a resposta
    return cliente.Adapt<Resposta>();
  }

  public void Excluir(int id)
  {
    //Buscar o cliente pelo id
    var cliente = BuscarPeloId(id);

    //Mandar o repositorio remover o cliente
    _repositorio.Excluir(cliente);
  }

  private Cliente BuscarPeloId(int id, bool tracking = true)
  {
    var cliente = _repositorio.Buscar(id, tracking);

    if (cliente is null)
    {
      throw new Exception("Cliente não encontrado");
    }

    return cliente;
  }
}
