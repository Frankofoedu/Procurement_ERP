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
    public class CategoryController : Controller
    {
        private BSSLSYS_ITF_DEMOContext _bsslContext;
        public CategoryController(BSSLSYS_ITF_DEMOContext bsslContext)
        {
            _bsslContext = bsslContext;
        }
        public PartialViewResult GetCatCode()
        {
            //get all categories 
            var catList = _bsslContext.Category.ToList();

            //var  = _bsslContext.Stafftab.ToList();
            return new PartialViewResult
            {
                ViewName = "Modals/_CatCodeLayout",
                ViewData = new ViewDataDictionary<List<Category>>(ViewData, catList)
            };
        }
    }
}