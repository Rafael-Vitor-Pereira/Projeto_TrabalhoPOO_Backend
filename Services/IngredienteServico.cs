using Mapster;
using Microsoft.AspNetCore.Mvc;
using TrabalhoPOO.Dtos.Ingrediente;
using TrabalhoPOO.Models;
using TrabalhoPOO.Repositores;

namespace TrabalhoPOO.Services;

public class IngredienteServico{
	private readonly IngredienteRepositorio _ingredienteRepositorio;

	public IngredienteServico([FromServices] IngredienteRepositorio repositorio){
		_ingredienteRepositorio = repositorio;
	}

	public Resposta CriarIngrediente(CriarAtualizarRequisicao novoIngrediente){
		var ingrediente = novoIngrediente.Adapt<Ingrediente>();

		_ingredienteRepositorio.CriarIngrediente(ingrediente);

		var resposta = ingrediente.Adapt<Resposta>();

		return resposta;
	}

	public List<Resposta> ListarIngrediente(){
		var ingredientes = _ingredienteRepositorio.ListarIngrediente();

		var respostas = ingredientes.Adapt<List<Resposta>>();

		return respostas;
	}

	public void Remover(int id){
		var ingrediente  = BuscarPeloId(id);

		_ingredienteRepositorio.Remover(ingrediente);
	}

	public Resposta Buscar(int id){
		var ingrediente  = BuscarPeloId(id, false);

		return ingrediente.Adapt<Resposta>();
	}

	public Resposta Atualizar(int id, CriarAtualizarRequisicao ingredienteEditado){
		var ingrediente  = BuscarPeloId(id);

		ingredienteEditado.Adapt(ingrediente);

		_ingredienteRepositorio.Atualizar();

		return ingrediente.Adapt<Resposta>();
	}

	private Ingrediente BuscarPeloId(int id, bool tracking = true){
		var ingrediente = _ingredienteRepositorio.Buscar(id, tracking);

		if(ingrediente is null){
			throw new Exception("Ingrediente não encontrado!");
		}

		return ingrediente;
	}
}