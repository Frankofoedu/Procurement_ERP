using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DcProcurement;

namespace BsslProcurement.Pages.Staff
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcurementCategoriesController : ControllerBase
    {
        private readonly ProcurementDBContext _context;

        public ProcurementCategoriesController(ProcurementDBContext context)
        {
            _context = context;
        }

        // GET: api/ProcurementCategories
        [HttpGet]
        public IEnumerable<ProcurementCategory> GetProcurementCategories()
        {
            return _context.ProcurementCategories;
        }

        // GET: api/ProcurementCategories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProcurementCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var procurementCategory = await _context.ProcurementCategories.FindAsync(id);

            if (procurementCategory == null)
            {
                return NotFound();
            }

            return Ok(procurementCategory);
        }

        // PUT: api/ProcurementCategories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcurementCategory([FromRoute] int id, [FromBody] ProcurementCategory procurementCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != procurementCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(procurementCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcurementCategoryExists(id))
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

        // POST: api/ProcurementCategories
        [HttpPost]
        public async Task<IActionResult> PostProcurementCategory([FromBody] ProcurementCategory procurementCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProcurementCategories.Add(procurementCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcurementCategory", new { id = procurementCategory.Id }, procurementCategory);
        }

        // DELETE: api/ProcurementCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcurementCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var procurementCategory = await _context.ProcurementCategories.FindAsync(id);
            if (procurementCategory == null)
            {
                return NotFound();
            }

            _context.ProcurementCategories.Remove(procurementCategory);
            await _context.SaveChangesAsync();

            return Ok(procurementCategory);
        }

        private bool ProcurementCategoryExists(int id)
        {
            return _context.ProcurementCategories.Any(e => e.Id == id);
        }
    }
}