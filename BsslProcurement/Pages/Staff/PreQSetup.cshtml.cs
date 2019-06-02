using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DcProcurement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BsslProcurement.Pages.Staff
{
    public class PreQSetupModel : PageModel
    {
        private readonly ProcurementDBContext _context;

        public PreQSetupModel(ProcurementDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PrequalificationPolicy PrequalificationPolicy { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
       
        [BindProperty]
        public bool isSetupAlready { get; set; }

        public void OnGet()
        {
            if (CheckIfPrequalificationSetup())
            {
                Error = "Prequalification Has been setup already";
                isSetupAlready = true;
                var existingPolicy = _context.PrequalificationPolicies.FirstOrDefault();
                if (existingPolicy != null)
                {
                    PrequalificationPolicy = existingPolicy;
                }
            }
            else
            {
                isSetupAlready = false;
            }
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Error = "An error has occured. Please check the data entered.";

                return;
            }
            if (isSetupAlready)
            {

                var oldSetup = _context.PrequalificationPolicies.FirstOrDefault();
                PrequalificationPolicy.Id = oldSetup.Id;
                _context.Entry(oldSetup).CurrentValues.SetValues(PrequalificationPolicy);
               // oldSetup = PrequalificationPolicy;
                Message = "Updated setup!";
            }
            else
            {
                _context.PrequalificationPolicies.Add(PrequalificationPolicy);
                Message = "Setup done!";
            }

            _context.SaveChanges();

        }
        /// <summary>
        /// checks if the prequalification has been setup for the present year
        /// </summary>
        /// <returns>returns true or false</returns>
        bool CheckIfPrequalificationSetup()
        {
            return _context.PrequalificationPolicies.Any(x =>
                        x.PrequalificationEndDate.Year >= DateTime.Now.Year
                         );

        }
    }
}