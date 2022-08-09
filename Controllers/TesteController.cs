using MeuTeste.Data;
using MeuTeste.Models;
using MeuTeste.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuTeste.Controllers
//recebe requisição, manipula e retorna para a tela
{
    [ApiController]
    [Route("v1")] //template
    public class TesteController : ControllerBase
    {
        // criar a primeira action - retornando uma lista do Teste
        // metodo HTTPGET - Select geral
        [HttpGet]
        [Route("testes")] //template
        public async Task<IActionResult> GetAsync( //metodo assincrono (await)
            [FromServices] AppDbContext context)
        {
            //List<Teste>
            var testes = await context  //AppDbContext
                        .Testes         //DbSet<Teste>
                        .AsNoTracking() //IQueryable<Teste> - não efetuar rastreio - item de performance
                        .ToListAsync(); //Task<List<...>>
            return Ok(testes);
        }
        // metodo HTTPGET - Select Unico
        [HttpGet]
        [Route("testes/{id}")] //template recebe via parametro id
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var testid = await context
                        .Testes
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id); //x:Teste
            return testid == null ? NotFound() : Ok(testid);
        }
        // metodo HTTPPOST - Inserção
        [HttpPost("testes")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateTesteViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var teste = new Teste
            {
                Data = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                await context.Testes.AddAsync(teste);
                await context.SaveChangesAsync();
                return Created($"v1/testes/{teste.Id}", teste);

            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        // metodo HTTPPUT - Atualização
        [HttpPut("testes/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateTesteViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var teste = await context
                              .Testes
                              .FirstOrDefaultAsync(x => x.Id == id);

            if (teste == null)
                return NotFound();

            try
            {
                teste.Title = model.Title;
                context.Testes.Update(teste);
                await context.SaveChangesAsync();
                return Ok(teste);

            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        // metodo HTTPDELETE - Apagar
        [HttpDelete("testes/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var teste = await context
                              .Testes
                              .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Testes.Remove(teste);
                await context.SaveChangesAsync();
                return Ok(teste);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

    }
}
