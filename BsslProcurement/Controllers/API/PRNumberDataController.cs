using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BsslProcurement.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PRNumberDataController : ControllerBase
    {
        private readonly IPRNumberService _pRNumberService;
        public PRNumberDataController( IPRNumberService pRNumberService  )
        {
            _pRNumberService = pRNumberService;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetPRNumberData([FromRoute] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prnumberdata = await _pRNumberService.GetDashedPRNumberAndDepartmentDataAsync(code);

            return Ok(prnumberdata);
        }


    }
}