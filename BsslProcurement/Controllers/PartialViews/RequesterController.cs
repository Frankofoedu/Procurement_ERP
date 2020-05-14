using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.ViewModels;
using DcProcurement.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BsslProcurement.Controllers.PartialViews
{

    [Route("[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RequesterController : Controller
    {
        private readonly BSSLSYS_ITF_DEMOContext _bsslContext;

        public RequesterController(BSSLSYS_ITF_DEMOContext bsslContext)
        {
            _bsslContext = bsslContext;
        }
        public async Task<PartialViewResult> GetRequester(string type)
        {
            //get requester

            var requesters = new List<RequesterViewModel>();

            if (type.ToLower() == "department")
            {
                requesters = _bsslContext.Codestab.Where(opt => opt.Option1 == "F5")
                    .Select(m => new RequesterViewModel() { code = m.Code, name = m.Desc1, type = type }).ToList();
            }
            else if (type.ToLower() == "division")
            {
                requesters = _bsslContext.Codestab.Where(opt => opt.Option1 == "Div")
                    .Select(m => new RequesterViewModel() { code = m.Code, name = m.Desc1, type = type }).ToList();
            }
            else if (type.ToLower() == "section")
            {
                requesters = _bsslContext.Codestab.Where(opt => opt.Option1 == "SECT")
                    .Select(m => new RequesterViewModel() { code = m.Code, name = m.Desc1, type = type }).ToList();
            }
            else if (type.ToLower() == "unit")
            {
                requesters = _bsslContext.Codestab.Where(opt => opt.Option1 == "Z16")
                    .Select(m => new RequesterViewModel() { code = m.Code, name = m.Desc1, type = type }).ToList();
            }
            else if (type.ToLower() == "staff")
            {
                requesters = _bsslContext.Stafftab.Select(x => new RequesterViewModel { code = x.Staffid, name = x.Surname + x.Othernames, type = type }).ToList();
            }

            return new PartialViewResult
            {
                ViewName = "Modals/_RequesterPartial",
                ViewData = new ViewDataDictionary<List<RequesterViewModel>>(ViewData, requesters)
            };
        }
    }
}