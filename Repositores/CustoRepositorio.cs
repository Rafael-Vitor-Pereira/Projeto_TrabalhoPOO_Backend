using TrabalhoPOO.Data;
using TrabalhoPOO.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrabalhoPOO.Repositores;

public class CustoRepositorio{
    private ContextoBD _contexto;

	public CustoRepositorio([FromServices] ContextoBD contexto){
		_contexto = contexto;
	}

	public Custo CriarCusto(Custo custo){
		_contexto.Custos.Add(custo);
		_contexto.SaveChanges();

		return custo;
	}

	public List<Custo> ListarCusto(){
		return _contexto.Custos.ToList();
	}

	public Custo Buscar(int id){
		return _contexto.Custos.FirstOrDefault(custo => custo.Id == id);
	}

	public void Remover(Custo custo){
		_contexto.Remove(custo);
		_contexto.SaveChanges();
	}

	public void Atualizar(){
		_contexto.SaveChanges();
	}
}
