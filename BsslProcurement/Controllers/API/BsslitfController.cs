using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
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
        private readonly IBsslITFService bsslITFService;
        public BsslitfController(IBsslITFService bsslITFService)
        {
            this.bsslITFService = bsslITFService;
        }

        [HttpGet("Subcategory/{categoryCode}")]
        public async Task<IActionResult> GetSubcategory([FromRoute] string categoryCode) //id is the workflow category id
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subcategories = await bsslITFService.GetSubcategoriesAsync(categoryCode);

            return Ok(subcategories);
        }

        [HttpGet("LastSupplier/{itemCode}")]
        public async Task<IActionResult> GetLastSupplier([FromRoute] string itemCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rtn = await bsslITFService.GetLastSupplierAsync(itemCode);

            if (rtn != null)
            {
                Ok(rtn);
            }

            return NotFound("Supplier not found.");
        }
    }
}