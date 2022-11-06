using Mapster;
using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Atendente;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class AtendenteServico{
	private readonly AtendenteRepositorio _atendenteRepositorio;

	public AtendenteServico([FromServices] AtendenteRepositorio repositorio){
		_atendenteRepositorio = repositorio;
	}

	public Resposta CriarAtendente(CriarAtualizarRequisicao novoAtendente){
		var atendente = novoAtendente.Adapt<Atendente>();

		var agora = DateTime.Now;
		atendente.DataCadastro = agora;

		_atendenteRepositorio.CriarAtendente(atendente);

		var resposta = atendente.Adapt<Resposta>();

		return resposta;
	}

	public List<Resposta> ListarAtendente(){
		var atendentes = _atendenteRepositorio.ListarAtendente();

		var respostas = atendentes.Adapt<List<Resposta>>();

		return respostas;
	}

	public void Remover(int id){
		var atendente  = BuscarPeloId(id);

		_atendenteRepositorio.Remover(atendente);
	}

	public Resposta Buscar(int id){
		var atendente  = BuscarPeloId(id, false);

		return atendente.Adapt<Resposta>();
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao atendenteEditado){
		var atendente  = BuscarPeloId(id);

		atendenteEditado.Adapt(atendente);

		_atendenteRepositorio.Atualizar();

		return atendente.Adapt<Resposta>();
	}

	private Atendente BuscarPeloId(int id, bool tracking = true){
		var atendente = _atendenteRepositorio.Buscar(id, tracking);

		if(atendente is null){
			throw new Exception("Atendente não encontrado!");
		}

		return atendente;
	}
}