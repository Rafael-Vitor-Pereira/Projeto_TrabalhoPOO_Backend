using TrabalhoPOO.Data;
using TrabalhoPOO.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrabalhoPOO.Repositores;

public class IngredienteRepositorio{
    private ContextoBD _contexto;

	public IngredienteRepositorio([FromServices] ContextoBD contexto){
		_contexto = contexto;
	}

	public Ingrediente CriarIngrediente(Ingrediente ingrediente){
		_contexto.Ingredientes.Add(ingrediente);
		_contexto.SaveChanges();

		return ingrediente;
	}

	public List<Ingrediente> ListarIngrediente(){
		return _contexto.Ingredientes.ToList();
	}

	public Ingrediente Buscar(int id){
		return _contexto.Ingredientes.FirstOrDefault(ingrediente => ingrediente.Id == id);
	}

	public void Remover(Ingrediente ingrediente){
		_contexto.Remove(ingrediente);
		_contexto.SaveChanges();
	}

	public void Atualizar(){
		_contexto.SaveChanges();
	}
}
