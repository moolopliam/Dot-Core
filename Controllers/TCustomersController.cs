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
    public class TCustomersController : ControllerBase
    {
        private readonly TestpContext _context;

        public TCustomersController(TestpContext context)
        {
            _context = context;
        }

        // GET: api/TCustomers
        [HttpGet]
        public IEnumerable<object> GetTCustomer()
        {

            //var Customer = from Cus in _context.TCustomer
            //               join Tttel in _context.TTitel on Cus.CustId equals Tttel.TCustomer
            //               join Ty in _context.TType on 
            var Customer = from Ty in _context.TType
                           join Cus in _context.TCustomer on Ty.CustType equals Cus.CustType
                           join ti in _context.TTitel on Cus.InitialCode equals ti.InitialCode
                           select new
                           {
                               ti.InitialCode,
                               ti.InitialName,
                               Cus.CustId,
                               Cus.Name,
                               Cus.Lastname,
                               Ty.CustType,
                               Ty.NameType

                           };

            return Customer;
        }
        // GET: api/TCustomers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTCustomer([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tCustomer = await _context.TCustomer.FindAsync(id);

            if (tCustomer == null)
            {
                return NotFound();
            }

            return Ok(tCustomer);
        }

        // PUT: api/TCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTCustomer([FromRoute] string id, [FromBody] TCustomer tCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tCustomer.CustId)
            {
                return BadRequest();
            }

            _context.Entry(tCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TCustomerExists(id))
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

        // POST: api/TCustomers
        [HttpPost]
        public async Task<IActionResult> PostTCustomer([FromBody] TCustomer tCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TCustomer.Add(tCustomer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TCustomerExists(tCustomer.CustId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTCustomer", new { id = tCustomer.CustId }, tCustomer);
        }

        // DELETE: api/TCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTCustomer([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tCustomer = await _context.TCustomer.FindAsync(id);
            if (tCustomer == null)
            {
                return NotFound();
            }

            _context.TCustomer.Remove(tCustomer);
            await _context.SaveChangesAsync();

            return Ok(tCustomer);
        }

        private bool TCustomerExists(string id)
        {
            return _context.TCustomer.Any(e => e.CustId == id);
        }
    }
}