using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.UtilityMethods;
using DcProcurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BsslProcurement.Pages.Staff.Administration
{
    [Authorize]
    public class UserMigrationModel : PageModel
    {
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly DcProcurement.ProcurementDBContext _procContext;
        private readonly ILogger<UserMigrationModel> _logger;

        private readonly UserManager<User> _userManager;
        public int StaffCount { get; private set; }
        public int? SuccessCount { get; private set; }
        public int? DuplicateCount { get; private set; }

        public UserMigrationModel(DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext bsslContext,
            DcProcurement.ProcurementDBContext procContext,
            UserManager<User> userManager,
            ILogger<UserMigrationModel> logger)
        {
            _procContext = procContext;
            _bsslContext = bsslContext;
            _userManager = userManager;

            _logger = logger;
        }
        public void OnGet()
        {

        }
        public async Task OnPostAddUsers()
        {
            await MigrateStaffFromUserAcctToIdentity();
        }

        private async Task MigrateStaffFromUserAcctToIdentity()
        {
            var oldstaff = await _bsslContext.Useracct.ToListAsync();

            SuccessCount = 0;
            DuplicateCount = 0;
            foreach (var staff in oldstaff)
            {
                await signUpAsync("", staff.Username, staff.Userid, staff.Pwd);
            }
            StaffCount = oldstaff.Count;
        }

        async Task signUpAsync(string email, string name, string staffcode, string password)
        {
            
            string em = GetEmail(email);

            if (name == null)
            {
                return;
            }
            var user = new DcProcurement.Staff
            {
                UserName = staffcode.Trim(),
                Email = em,
                CreationDate = DateTime.Now,
                Name = name.Trim(),
                StaffCode = staffcode.Trim(),
            };
            var result = await _userManager.CreateAsync(user, password.Trim());
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                SuccessCount++;
            }
            else
            {
                if (result.Errors.Any(x=> x.Code == "DuplicateUserName"))
                {
                    DuplicateCount++;
                }
                _logger.LogInformation("An error occured");
            }
        }

        private string GetEmail(string email)
        {
            return string.IsNullOrWhiteSpace(email) ? $"{GenerateRandomNo().ToString()}bsslTest@gmail.com" : email;
        }
        private int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            var _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}