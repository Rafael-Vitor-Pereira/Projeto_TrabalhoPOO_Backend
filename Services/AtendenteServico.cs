using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Atendente;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class AtendenteServico{
	private AtendenteRepositorio _atendenteRepositorio;

	public AtendenteServico([FromServices] AtendenteRepositorio repositorio){
		_atendenteRepositorio = repositorio;
	}

	public Resposta CriarAtendente(CriarAtualizarRequisicao novoAtendente){
		var atendente = new Atendente();
		RequisicaoParaModelo(novoAtendente, atendente);

		var agora = DateTime.Now;
		atendente.DataCadastro = agora;

		_atendenteRepositorio.CriarAtendente(atendente);

		var resposta = ModeloParaResposta(atendente);

		return resposta;
	}

	public List<Resposta> ListarAtendente(){
		var atendentes = _atendenteRepositorio.ListarAtendente();

		List<Resposta> atendenteResposta = new();

		foreach (var pessoa in atendentes)
		{
			var resposta = ModeloParaResposta(pessoa);

			atendenteResposta.Add(resposta);
		}

		return atendenteResposta;
	}

	public void Remover(int id){
		var atendente  = BuscarPeloId(id);

		_atendenteRepositorio.Remover(atendente);
	}

	private Resposta ModeloParaResposta(Atendente modelo){
		var resposta = new Resposta();
		resposta.Id = modelo.Id;
		resposta.Nome = modelo.Nome;
		resposta.Email = modelo.Email;
		resposta.Telefone = modelo.Telefone;
		resposta.Usuario = modelo.Usuario;
		resposta.DataCadastro = modelo.DataCadastro;

		return resposta;
	}

	public Resposta Buscar(int id){
		var atendente  = BuscarPeloId(id);

		return ModeloParaResposta(atendente);
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao atendenteEditado){
		var atendente  = BuscarPeloId(id);

		RequisicaoParaModelo(atendenteEditado, atendente);

		_atendenteRepositorio.Atualizar();

		return ModeloParaResposta(atendente);
	}

	private void RequisicaoParaModelo(CriarAtualizarRequisicao requisicao, Atendente modelo){
		modelo.Nome = requisicao.Nome;
		modelo.Email = requisicao.Email;
		modelo.Telefone = requisicao.Telefone;
		modelo.Usuario = requisicao.Usuario;
		modelo.Senha = requisicao.Senha;
	}

	private Atendente BuscarPeloId(int id){
		var atendente = _atendenteRepositorio.Buscar(id);

		if(atendente is null){
			throw new Exception("Atendente não encontrado!");
		}

		return atendente;
	}
}