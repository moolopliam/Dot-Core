using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEST.Models;

namespace TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TProductsController : ControllerBase
    {
        private readonly TestpContext _context;

        public TProductsController(TestpContext context)
        {
            _context = context;
        }

        // GET: api/TProducts
        [HttpGet]
        public IEnumerable<object> GetTProduct()
        {
            var Product = from St in _context.TSatus
                          join Por in _context.TProduct on St.StatusId equals Por.StatusId
                          join Ct in _context.TCatategory on Por.CatId equals Ct.CatId
                          join Un in _context.TUnit on Por.UnitCode equals Un.UnitCode
                          select new
                          {
                              Por.Code,
                              Por.Name,
                              Por.Price,
                              Por.Qty,
                              Por.UnitPerPrice,
                              Un.UnitCode,
                              Un.NameUnit,
                              Ct.CatId,
                              Ct.NameCat,
                              St.StatusId,
                              St.StatusName,
                          };
            return Product;
        }

        // GET: api/TProducts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTProduct([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tProduct = await _context.TProduct.FindAsync(id);

            if (tProduct == null)
            {
                return NotFound();
            }

            return Ok(tProduct);
        }

        // PUT: api/TProducts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTProduct([FromRoute] string id, [FromBody] TProduct tProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tProduct.Code)
            {
                return BadRequest();
            }

            _context.Entry(tProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TProducts
        [HttpPost]
        public async Task<IActionResult> PostTProduct([FromBody] TProduct tProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TProduct.Add(tProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TProductExists(tProduct.Code))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTProduct", new { id = tProduct.Code }, tProduct);
        }

        // DELETE: api/TProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTProduct([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tProduct = await _context.TProduct.FindAsync(id);
            if (tProduct == null)
            {
                return NotFound();
            }

            _context.TProduct.Remove(tProduct);
            await _context.SaveChangesAsync();

            return Ok(tProduct);
        }

        private bool TProductExists(string id)
        {
            return _context.TProduct.Any(e => e.Code == id);
        }
    }
}