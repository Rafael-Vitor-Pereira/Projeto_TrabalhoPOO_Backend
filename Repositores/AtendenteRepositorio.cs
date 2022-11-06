using TrabalhoPOO.Data;
using TrabalhoPOO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TrabalhoPOO.Repositores;

public class AtendenteRepositorio{
	private readonly ContextoBD _contexto;

	public AtendenteRepositorio([FromServices] ContextoBD contexto){
		_contexto = contexto;
	}

	public Atendente CriarAtendente(Atendente atendente){
		_contexto.Atendentes.Add(atendente);
		_contexto.SaveChanges();

		return atendente;
	}

	public List<Atendente> ListarAtendente(){
		return _contexto.Atendentes.AsNoTracking().ToList();
	}

	public Atendente Buscar(int id, bool tracking = true){
		return (tracking) ? _contexto.Atendentes.FirstOrDefault(atendente => atendente.Id == id) : _contexto.Atendentes.AsNoTracking().FirstOrDefault(atendente => atendente.Id == id);	
	}

	public void Remover(Atendente atendente){
		_contexto.Remove(atendente);
		_contexto.SaveChanges();
	}

	public void Atualizar(){
		_contexto.SaveChanges();
	}
}