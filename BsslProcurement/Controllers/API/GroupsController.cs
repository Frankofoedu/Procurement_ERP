using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BsslProcurement.Controllers.API
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupManagement _groupManagement;
        public GroupsController(IGroupManagement groupManagement)
        {
            _groupManagement = groupManagement;
        }
        [HttpGet("GetAllGroups")]
        public async Task<ActionResult<List<GroupViewModel>>> GetGroup()
        {
            var gm = await _groupManagement.GetAll();

            if (gm == null)
            {
                return NotFound("No data found");
            }

            return gm.Select(x => new GroupViewModel { Name = x.GroupName, Id = x.Id }).ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(long id)
        {
            var gm = await _groupManagement.GetById(id);

            if (gm == null)
            {
                return NotFound();
            }

            return GroupViewModel(gm);
        }

        [HttpPost("NewGroup")]
        public async Task<ActionResult> CreateGroup(GroupViewModel gvm)
        {
            try
            {
                var result = _groupManagement.CreateGroup(gvm.Name);

                return CreatedAtAction(nameof(GetGroup), new { id = result.Id }, result);
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

        [HttpDelete("RemoveGroup")]
        public async Task<ActionResult> Remove(long id)
        {
            await _groupManagement.DeleteGroup(id);

            return NoContent();
        }

        private static GroupViewModel GroupViewModel(UserGroup ug) =>
            new GroupViewModel
            {
                Id = ug.Id,
                Name = ug.GroupName
            };
    }

   


}