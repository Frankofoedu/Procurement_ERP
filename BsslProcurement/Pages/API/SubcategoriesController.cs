using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DcProcurement;

namespace BsslProcurement.Pages.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly ProcurementDBContext _context;

        public SubcategoriesController(ProcurementDBContext context)
        {
            _context = context;
        }

        // GET: api/Subcategories
        //[HttpGet]
        //public IEnumerable<ProcurementSubcategory> GetProcurementSubcategories()
        //{
        //    return _context.ProcurementSubcategories;
        //}

        // GET: api/Subcategories/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetProcurementSubcategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState));
            }

            var procurementSubcategory = await _context.ProcurementSubcategories.FindAsync(id);

            if (procurementSubcategory == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(procurementSubcategory);
        }

        // PUT: api/Subcategories/5
       // [HttpPut("{id}")]
        //public async Task<IActionResult> PutProcurementSubcategory([FromRoute] int id, [FromBody] ProcurementSubcategory procurementSubcategory)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != procurementSubcategory.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(procurementSubcategory).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProcurementSubcategoryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Subcategories
        //[HttpPost]
        //public async Task<IActionResult> PostProcurementSubcategory([FromBody] ProcurementSubcategory procurementSubcategory)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.ProcurementSubcategories.Add(procurementSubcategory);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProcurementSubcategory", new { id = procurementSubcategory.Id }, procurementSubcategory);
        //}

        // DELETE: api/Subcategories/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProcurementSubcategory([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var procurementSubcategory = await _context.ProcurementSubcategories.FindAsync(id);
        //    if (procurementSubcategory == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.ProcurementSubcategories.Remove(procurementSubcategory);
        //    await _context.SaveChangesAsync();

        //    return Ok(procurementSubcategory);
        //}

        private bool ProcurementSubcategoryExists(int id)
        {
            return _context.ProcurementSubcategories.Any(e => e.Id == id);
        }
    }
}