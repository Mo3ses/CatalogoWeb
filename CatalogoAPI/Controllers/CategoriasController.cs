using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CatalogoAPI.Context;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CatalogoAPI.Controllers
{
    [Route("[controller]")]
    public class CategoriasController : Controller
    {
        private readonly ILogger<CategoriasController> _logger;
        private readonly AppDbContext _context;

        public CategoriasController(ILogger<CategoriasController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            List<Categoria> categorias = _context.Categorias.AsNoTracking().ToList();
            if (!categorias.Any()) { return NotFound(); }

            return Ok(categorias);
        }
        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            Categoria? categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (categoria is null) { return NotFound("Categoria não encontrada."); }

            return Ok(categoria);
        }
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            List<Categoria> categorias = _context.Categorias.Include(c => c.Produtos).ToList();
            if (!categorias.Any()) { return NotFound(); }

            return Ok(categorias);
        }
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            if (categoria is null) { return BadRequest(); }
            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.Id }, categoria);

        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id) { return BadRequest(); }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            Categoria? categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria is null) { return NotFound("Categoria não encontrada."); }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}