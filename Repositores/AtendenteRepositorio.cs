using TrabalhoPOO.Data;
using TrabalhoPOO.Models;
using Microsoft.AspNetCore.Mvc;

namespace TrabalhoPOO.Repositores;

public class AtendenteRepositorio{
	private ContextoBD _contexto;

	public AtendenteRepositorio([FromServices] ContextoBD contexto){
		_contexto = contexto;
	}

	public Atendente CriarAtendente(Atendente atendente){
		_contexto.Atendentes.Add(atendente);
		_contexto.SaveChanges();

		return atendente;
	}

	public List<Atendente> ListarAtendente(){
		return _contexto.Atendentes.ToList();
	}

	public Atendente Buscar(int id){
		return _contexto.Atendentes.FirstOrDefault(atendente => atendente.Id == id);
	}

	public void Remover(Atendente atendente){
		_contexto.Remove(atendente);
		_contexto.SaveChanges();
	}

	public void Atualizar(){
		_contexto.SaveChanges();
	}
}