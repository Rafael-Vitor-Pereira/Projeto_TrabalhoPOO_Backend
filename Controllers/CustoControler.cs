// using Microsoft.AspNetCore.Mvc;
// using Back_end.Dtos.Custo;
// using Back_end.Services;

// namespace Back_end.Controllers;

// [ApiController]
// [Route("custo")]

// public class CustoController : ControllerBase
// {
//   private readonly CustoServico _custoServico;

//   public CustoController([FromServices] CustoServico servico)
//   {
//     _custoServico = servico;
//   }

//   [HttpPost]
//   public ActionResult<Resposta> Cadastro([FromBody] CriarRequisicao novoCusto)
//   {
//     var resposta = _custoServico.Cadastrar(novoCusto);
//     return CreatedAtAction(nameof(Buscar), new { id = resposta.Id }, resposta);
//   }

//   [HttpGet]
//   public ActionResult<List<Resposta>> Listar()
//   {
//     return Ok(_custoServico.ListarCusto());
//   }

//   [HttpGet("{id:int}")]
//   public ActionResult<Resposta> Buscar([FromRoute] int id)
//   {
//     try
//     {
//       return Ok(_custoServico.Buscar(id));
//     }
//     catch (Exception e)
//     {
//       return NotFound(new { mensagem = e.Message });
//     }

//   }

//   [HttpDelete("{id:int}")]
//   public ActionResult Delete([FromRoute] int id)
//   {
//     try
//     {
//       _custoServico.Remover(id);

//       return NoContent();
//     }
//     catch (Exception e)
//     {
//       return NotFound(new { mensagem = e.Message });
//     }

//   }

//   [HttpPut("{id:int}")]
//   public ActionResult<Resposta> Editar([FromRoute] int id, [FromBody] AtualizarRequisicao custoEditado)
//   {
//     try
//     {
//       return Ok(_custoServico.Atualizar(id, custoEditado));
//     }
//     catch (Exception e)
//     {
//       return NotFound(new { mensagem = e.Message });
//     }

//   }
// }