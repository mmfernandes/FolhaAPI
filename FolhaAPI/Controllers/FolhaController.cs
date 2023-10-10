using FolhaAPI.Data;
using FolhaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[ApiController]
[Route("api/folha")]
public class FolhaController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public FolhaController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult Listar()
    {
        try
        {
            List<Folha> folhas = _ctx
                .Folhas
                .Include(x => x.Funcionario)
                .ToList();

            return folhas.Count == 0 ? NotFound("Não existe folha cadastrada!") : Ok(folhas);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("buscar/{cpf}/{mes}/{ano}")]
    public IActionResult Buscar([FromRoute] string cpf, int mes, int ano)
    {
        try
        {
            Funcionario? funcionario = _ctx.Funcionarios.FirstOrDefault(x => x.CPF == cpf);
            
            if (funcionario == null)
            {
                return NotFound("Funcionario não encontrado");
            }

            Folha? folha = _ctx.Folhas.FirstOrDefault(x => x.Mes == mes && x.Ano == ano && x.FuncionarioId == funcionario.FuncionarioId);
            
            if (folha != null)
            {
                return Ok(folha);
            }

            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult Post([FromBody] Folha folha)
    {
        try
        {
            folha.Funcionario = _ctx.Funcionarios.Find(folha.FuncionarioId);

            double salarioBruto = folha.Quantidade * folha.Valor;
            double irrf = 0.0;
            double inss = 0.0;
            double fgts = salarioBruto * 0.08;

            if (salarioBruto >= 1903.99 && salarioBruto <= 2826.65) {
                double aliquota = salarioBruto * 0.075;
                irrf = aliquota - 142.80;
            } else if (salarioBruto > 2826.66 && salarioBruto <= 3751.05) {
                double aliquota = salarioBruto * 0.15;
                irrf = aliquota - 354.80;
            } else if (salarioBruto > 3751.06 && salarioBruto <= 4664.68) {
                double aliquota = salarioBruto * 0.225;
                irrf = aliquota - 636.13;
            } else if (salarioBruto > 4664.68) {
                double aliquota = salarioBruto * 0.275;
                irrf = aliquota - 869.36;
            }

            if (salarioBruto <= 1693.72) {
                inss = salarioBruto * 0.08;
            } else if (salarioBruto >= 1693.73 && salarioBruto <= 2822.90) {
                inss = salarioBruto * 0.09;
            } else if (salarioBruto >= 2822.91 && salarioBruto <= 5645.80) {
                inss = salarioBruto * 0.11;
            } else if (salarioBruto >= 5645.81) {
                inss = 621.03;
            }


            folha.SalarioBruto = salarioBruto;
            folha.ImpostoIrrf = irrf;
            folha.ImpostoInss = inss;
            folha.ImpostoFgts = fgts;
            folha.SalarioLiquido = salarioBruto - irrf - inss;
            _ctx.Folhas.Add(folha);
            _ctx.SaveChanges();
            return Created("", folha);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}