using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Custo;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class CustoServico
{
    private CustoRepositorio _custoRepositorio;

	public CustoServico([FromServices] CustoRepositorio repositorio){
		_custoRepositorio = repositorio;
	}

	public Resposta CriarCusto(CriarAtualizarRequisicao novoCusto){
		var custo = new Custo();
		RequisicaoParaModelo(novoCusto, custo);

		var agora = DateTime.Now;
		custo.Data = agora;

		_custoRepositorio.CriarCusto(custo);

		var resposta = ModeloParaResposta(custo);

		return resposta;
	}

	public List<Resposta> ListarCusto(){
		var custos = _custoRepositorio.ListarCusto();

		List<Resposta> custoResposta = new();

		foreach (var item in custos)
		{
			var resposta = ModeloParaResposta(item);

			custoResposta.Add(resposta);
		}

		return custoResposta;
	}

	public void Remover(int id){
		var custo = _custoRepositorio.Buscar(id);

		if(custo is null){
			return;
		}

		_custoRepositorio.Remover(custo);
	}

	private Resposta ModeloParaResposta(Custo modelo){
		var resposta = new Resposta();
		resposta.Id = modelo.Id;
		resposta.Tipo = modelo.Tipo;
		resposta.Quant = modelo.Quant;
		resposta.Valor = modelo.Valor;
		resposta.Data = modelo.Data;
		resposta.IngredienteId = modelo.IngredienteId;

		return resposta;
	}

	public Resposta Buscar(int id){
		var custo  = _custoRepositorio.Buscar(id);

		return ModeloParaResposta(custo);
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao custoEditado){
		var custo = _custoRepositorio.Buscar(id);

		if(custo is null){
			return null;
		}

		RequisicaoParaModelo(custoEditado, custo);

		_custoRepositorio.Atualizar();

		return ModeloParaResposta(custo);
	}

	private void RequisicaoParaModelo(CriarAtualizarRequisicao requisicao, Custo modelo){
		modelo.Tipo = requisicao.Tipo;
		modelo.Quant = requisicao.Quant;
		modelo.Valor = requisicao.Valor;
		modelo.Data = requisicao.Data;
		modelo.IngredienteId = requisicao.IngredienteId;
	}
}
