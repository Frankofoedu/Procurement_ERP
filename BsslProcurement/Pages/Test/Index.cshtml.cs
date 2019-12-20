﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BsslProcurement.Interfaces;
using DcProcurement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BsslProcurement.Pages.Test
{
    public class IndexModel : PageModel
    {

        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext _bsslContext;
        private readonly DcProcurement.ProcurementDBContext _procContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly IEmailSenderService _emailSender;

        public int StaffCount { get; set; }
        public int SuccessCount { get; set; }

        [BindProperty]
        public DateTime date { get; set; }
        public string EmailStatusMessage { get; private set; }

        [Required]
        [BindProperty]
        public string Email { get; private set; }

        public IndexModel(DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext bsslContext,
            DcProcurement.ProcurementDBContext procContext,
            UserManager<User> userManager,
            ILogger<IndexModel> logger,
            SignInManager<User> signInManager,
            IEmailSenderService emailSender)
        {
            _procContext = procContext;
            _bsslContext = bsslContext;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        public async Task OnGetAsync()
        {
   await MigrateStaffFromUserAcctToIdentity();
          //  await SendEmailAsync();
        }

        public void OnPost()
        {

        }
        private async Task SendEmailAsync()
        {
            var email = "Frankofoedu@gmail.com";

            var subject = "Email Test";

            var message = "This is a test message.";

            await _emailSender.SendEmailAsync(email, subject, message);

            EmailStatusMessage = "Send test email was successful.";

        }
        private async Task MigrateStaffFromUserAcctToIdentity()
        {
            var oldstaff = await _bsslContext.Useracct.ToListAsync();

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