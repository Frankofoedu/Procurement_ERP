using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using BsslProcurement.ViewModels;
using DcProcurement;
using DcProcurement.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace BsslProcurement.Controllers.API
{
    [Route("api/Groups")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupManagement _groupManagement;
        private readonly IWebHostEnvironment _env;
        public GroupsController(IGroupManagement groupManagement, IWebHostEnvironment env)
        {
        _env=env;
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
                var result = await _groupManagement.CreateGroup(gvm.Name);

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
        
        [HttpPost("GetPrediction")]
        public async Task<ActionResult> GetPredictionAsync([FromForm] string page, [FromForm] IFormFile audioFile)
        {
            try
            {
                var modelPath = "";
                if (page == "Home")
                {
                    modelPath = Path.Combine(_env.ContentRootPath, "data", "MLModelHome.zip");
                }
                else if (page == "cart")
                {
                    modelPath = Path.Combine(_env.ContentRootPath, "data", "MLModelCartPage.zip");
                }
                else if (page == "singleItem")
                {
                    modelPath = Path.Combine(_env.ContentRootPath, "data", "MLModelSingleItemPage.zip");
                }
                else if (page == "category")
                {
                    modelPath = Path.Combine(_env.ContentRootPath, "data", "MLModelCategory.zip");
                }
                else if (page == "pay")
                {
                    //var v = @"C:\Users\Frank\source\repos\YorubaModelML\YorubaPredictionAPI\Models\MLModelPayPage.zip";
                    modelPath = Path.Combine(_env.ContentRootPath, "data", "MLModelPayPage.zip");
                }



                var model = new ConsumeModel(modelPath);

                var uploadPath = Path.Combine(_env.ContentRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);

                if (audioFile.Length > 0)
                {
                    var filePath = Path.Combine(uploadPath, audioFile.FileName);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        await audioFile.CopyToAsync(fs);
                    }

                    var md = new ModelInput { ImageSource = filePath };

                    var result = model.Predict(md);
                    Directory.Delete(uploadPath, true);
                    return Ok(new { q = result.Prediction, p = result.Score.Max() });
                    //return Ok(_env.ContentRootPath);

                }

                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
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
