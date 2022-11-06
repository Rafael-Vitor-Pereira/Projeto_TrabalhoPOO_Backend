using Mapster;
using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Custo;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class CustoServico{
	private readonly CustoRepositorio _custoRepositorio;

	public CustoServico([FromServices] CustoRepositorio repositorio){
		_custoRepositorio = repositorio;
	}

	public Resposta CriarCusto(CriarAtualizarRequisicao novoCusto){
		var custo = novoCusto.Adapt<Custo>();

		var agora = DateTime.Now;
		custo.Data = agora;

		_custoRepositorio.CriarCusto(custo);

		var resposta = custo.Adapt<Resposta>();

		return resposta;
	}

	public List<Resposta> ListarCusto(){
		var custos = _custoRepositorio.ListarCusto();

		var respostas = custos.Adapt<List<Resposta>>();

		return respostas;
	}

	public void Remover(int id){
		var custo  = BuscarPeloId(id);

		_custoRepositorio.Remover(custo);
	}

	public Resposta Buscar(int id){
		var custo  = BuscarPeloId(id, false);

		return custo.Adapt<Resposta>();
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao custoEditado){
		var custo  = BuscarPeloId(id);

		custoEditado.Adapt(custo);

		_custoRepositorio.Atualizar();

		return custo.Adapt<Resposta>();
	}

	private Custo BuscarPeloId(int id, bool tracking = true){
		var custo = _custoRepositorio.Buscar(id, tracking);

		if(custo is null){
			throw new Exception("Custo não encontrado!");
		}

		return custo;
	}
}