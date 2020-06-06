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
    }
}