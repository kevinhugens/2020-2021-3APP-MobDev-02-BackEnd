using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileAPI.Data;
using MobileAPI.Models;

namespace MobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly BoodschapContext _context;

        public ProductController(BoodschapContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducten()
        {
            return await _context.Producten.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Producten.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }



        [HttpGet("naam/{naam}")]
        public async Task<ActionResult<Product>> GetProductByName(string naam)
        {
            var product = await _context.Producten.Where(x=>x.Naam.Contains(naam)).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpGet("namen/{naam}")]
        public async Task<ActionResult<List<Product>>> GetProductsByName(string naam)
        {
            var products = await _context.Producten.Where(x => x.Naam.Contains(naam)).ToListAsync<Product>();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }



        [HttpGet("nummer/{nummer}")]
        public async Task<ActionResult<Product>> GetProductByNumber(int nummer)
        {
            var product = await _context.Producten.Where(x => x.Nummer.Equals(nummer)).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        
        [HttpGet("cat/{cat}")]
        public async Task<ActionResult<List<Product>>> GetProductsBycat(string cat)
        {
            var products = await _context.Producten.Where(x => x.Categorie.Contains(cat)).ToListAsync<Product>();

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        [HttpGet("count/")]
        public async Task<ActionResult<int>> GetCount()
        {
            return await _context.Producten.CountAsync();
        }



    }
}
