using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Pages.Staff
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodesController : ControllerBase
    {
        private readonly ProcurementDBContext _context;

        public CodesController(ProcurementDBContext context)
        {
            _context = context;
        }

        // GET: api/Codes/Category/C001
        [HttpGet("Category/{code}")]
        public async Task<IActionResult> GetCategoryCode([FromRoute] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.ProcurementCategories.FirstOrDefaultAsync(m=>m.ProcurementCategoryCode==code);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // GET: api/Codes/Subcategory/C001
        [HttpGet("Subcategory/{code}")]
        public async Task<IActionResult> GetSubategoryCode([FromRoute] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subcategory = await _context.ProcurementSubcategories.Include(n=>n.ProcurementCategory).FirstOrDefaultAsync(m => m.ProcurementSubCategoryCode == code);

            if (subcategory == null)
            {
                return NotFound();
            }

            return Ok(subcategory);
        }

        // GET: api/Codes/Item/C001
        [HttpGet("Item/{code}")]
        public async Task<IActionResult> GetItemCode([FromRoute] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Items.FirstOrDefaultAsync(m => m.ItemCode == code);

            if (item == null)
            {
                return Ok();
            }

            return Ok(item);
        }

        // GET: api/Codes/Itemname/pen
        [HttpGet("Itemname/{str}")]
        public async Task<IActionResult> GetItemName([FromRoute] string str)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Items.FirstOrDefaultAsync(m => m.ItemName.Contains(str));
            
            if (item == null)
            {
                return Ok();
            }

            return Ok(item);
        }
    }
}