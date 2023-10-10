using FolhaAPI.Data;
using FolhaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/funcionario")]
public class FuncionarioController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public FuncionarioController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Funcionario> funcionarios = _ctx.Funcionarios.ToList();
            return funcionarios.Count == 0 ? NotFound("Não foi encontrado nenhum funcionario") : Ok(funcionarios);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Cadastrar([FromBody] Funcionario funcionario)
    {
        try
        {
            _ctx.Funcionarios.Add(funcionario);
            _ctx.SaveChanges();
            return Created("Funcionario cadastrado com sucesso", funcionario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}