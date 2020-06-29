using BsslProcurement.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BsslProcurement.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitionController: ControllerBase
    {
        private readonly IRequisitionService _requisitionService;
        public RequisitionController(IRequisitionService requisitionService    )
        {
            _requisitionService = requisitionService;
        }

        [HttpDelete("DeleteItem/{itemId}")]
        public async Task<ActionResult> DeleteRequisitionItem(int itemId)
        {
            try
            {
                await _requisitionService.DeleteRequisitionItem(itemId);
                return Ok("Deleted Item");
            }
            catch (ArgumentNullException)
            {
                return BadRequest($"An error occurred: menu is null");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }


        }
    }
}
