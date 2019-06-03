using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DcProcurement;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BsslProcurement.Pages.DynamicData
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryCriteriasController : ControllerBase
    {
        private readonly ProcurementDBContext _context;

        public SubCategoryCriteriasController(ProcurementDBContext context)
        {
            _context = context;
        }

        // GET: api/SubCategoryCriterias
        [HttpGet]
        public IEnumerable<SubCategoryCriteria> GetSubCategoryCriterias()
        {
            return _context.SubCategoryCriterias;
        }

        // GET: api/SubCategoryCriterias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryCriteria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subCategoryCriteria = await _context.SubCategoryCriterias.FindAsync(id);

            if (subCategoryCriteria == null)
            {
                return NotFound();
            }

            return Ok(subCategoryCriteria);
        }

        // GET: api/SubCategoryCriterias/{list of ints}
       [HttpGet("ListSubCategoryCriteria")]
        public IActionResult GetListSubCategoryCriteria([FromQuery] int[] ids)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subCategoryCriteria = _context.SubCategoryCriterias.Where(x => ids.Contains((int)x.ProcurementSubcategoryId)).ToList();

            if (!subCategoryCriteria.Any())
            {
                //return new JsonResult("None");
                return NotFound();
            }

            return Ok(subCategoryCriteria);
        }


        // PUT: api/SubCategoryCriterias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategoryCriteria([FromRoute] int id, [FromBody] SubCategoryCriteria subCategoryCriteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subCategoryCriteria.Id)
            {
                return BadRequest();
            }

            _context.Entry(subCategoryCriteria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryCriteriaExists(id))
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

        // POST: api/SubCategoryCriterias
        [HttpPost]
        public async Task<IActionResult> PostSubCategoryCriteria([FromBody] SubCategoryCriteria subCategoryCriteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SubCategoryCriterias.Add(subCategoryCriteria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubCategoryCriteria", new { id = subCategoryCriteria.Id }, subCategoryCriteria);
        }

        // DELETE: api/SubCategoryCriterias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoryCriteria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subCategoryCriteria = await _context.SubCategoryCriterias.FindAsync(id);
            if (subCategoryCriteria == null)
            {
                return NotFound();
            }

            _context.SubCategoryCriterias.Remove(subCategoryCriteria);
            await _context.SaveChangesAsync();

            return Ok(subCategoryCriteria);
        }

        private bool SubCategoryCriteriaExists(int id)
        {
            return _context.SubCategoryCriterias.Any(e => e.Id == id);
        }
    }
}