using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Ingrediente;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class IngredienteServico
{
    private IngredienteRepositorio _ingredienteRepositorio;

	public IngredienteServico([FromServices] IngredienteRepositorio repositorio){
		_ingredienteRepositorio = repositorio;
	}

	public Resposta CriarIngrediente(CriarAtualizarRequisicao novoIngrediente){
		var ingrediente = new Ingrediente();
		RequisicaoParaModelo(novoIngrediente, ingrediente);

		_ingredienteRepositorio.CriarIngrediente(ingrediente);

		var resposta = ModeloParaResposta(ingrediente);

		return resposta;
	}

	public List<Resposta> ListarIngrediente(){
		var ingredientes = _ingredienteRepositorio.ListarIngrediente();

		List<Resposta> ingredienteResposta = new();

		foreach (var pessoa in ingredientes)
		{
			var resposta = ModeloParaResposta(pessoa);

			ingredienteResposta.Add(resposta);
		}

		return ingredienteResposta;
	}

	public void Remover(int id){
		var ingrediente = _ingredienteRepositorio.Buscar(id);

		if(ingrediente is null){
			return;
		}

		_ingredienteRepositorio.Remover(ingrediente);
	}

	private Resposta ModeloParaResposta(Ingrediente modelo){
		var resposta = new Resposta();
		resposta.Id = modelo.Id;
		resposta.Nome = modelo.Nome;
		resposta.Valor = modelo.Valor;

		return resposta;
	}

	public Resposta Buscar(int id){
		var ingrediente  = _ingredienteRepositorio.Buscar(id);

		return ModeloParaResposta(ingrediente);
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao ingredienteEditado){
		var ingrediente = _ingredienteRepositorio.Buscar(id);

		if(ingrediente is null){
			return null;
		}

		RequisicaoParaModelo(ingredienteEditado, ingrediente);

		_ingredienteRepositorio.Atualizar();

		return ModeloParaResposta(ingrediente);
	}

	private void RequisicaoParaModelo(CriarAtualizarRequisicao requisicao, Ingrediente modelo){
		modelo.Nome = requisicao.Nome;
		modelo.Valor = requisicao.Valor;
	}
}
