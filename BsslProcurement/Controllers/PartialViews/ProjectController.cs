using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BsslProcurement.Controllers.PartialViews
{
    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ProjectController : Controller
    {
        private BSSLSYS_ITF_DEMOContext bsslContext;
        public ProjectController(BSSLSYS_ITF_DEMOContext _bsslContext)
        {
            bsslContext = _bsslContext;
        }

        public PartialViewResult GetProjects()
        {
            //get all projects 
            var projectList = bsslContext.Joborder.ToList();

            return new PartialViewResult
            {
                ViewName = "Modals/_ProjectsPartial",
                ViewData = new ViewDataDictionary<List<Joborder>>(ViewData, projectList)
            };
        }
    }
}