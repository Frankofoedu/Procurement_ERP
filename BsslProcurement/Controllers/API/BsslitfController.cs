using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BsslProcurement.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BsslitfController : ControllerBase
    {
        private readonly BSSLSYS_ITF_DEMOContext _bsslcontext;
        public BsslitfController(BSSLSYS_ITF_DEMOContext context)
        {
            _bsslcontext = context;
        }

        [HttpGet("Subcategory/{categoryCode}")]
        public async Task<IActionResult> GetSubcategory([FromRoute] string categoryCode) //id is the workflow category id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subcategories = await _bsslcontext.Busline.Where(m => m.CatCodes == categoryCode).ToListAsync();

            return Ok(subcategories);
        }

        [HttpGet("LastSupplier/{itemCode}")]
        public async Task<IActionResult> GetLastSupplier([FromRoute] string itemCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var arr = itemCode.Split('-');

            var lastSupply = await _bsslcontext.Gitab.Where(m => m.Groupno == arr[0] + arr[1] && m.Stockno == arr[2])
                .OrderByDescending(m=> m.Serno).FirstOrDefaultAsync();
            if (lastSupply != null)
            {
                var lastSupplier = await _bsslcontext.Accusts.Select(n=> new Accust { 
                    Keyid = n.Keyid,
                    Custcode = n.Custcode,
                    Accname = n.Accname
                }).FirstOrDefaultAsync(m => m.Custcode == lastSupply.Suppcode);

                if (lastSupplier != null)
                {
                    return Ok(lastSupplier);
                }
            }

            return NotFound("Supplier not found.");
        }
    }
}