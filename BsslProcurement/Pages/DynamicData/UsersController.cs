using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DcProcurement;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BsslProcurement.Pages.DynamicData
{
    public class UserAPIModel
    {
        public string name;
        public string staffCode;
        public string password;
        public string email;
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UsersController(
            UserManager<User> userManager,
            ILogger<UsersController> logger, 
            SignInManager<User> signInManager
            )
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }
        //[HttpPost]
        // public void Post([FromBody])


        //Generate 4 digit RandomNo
        private int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            var _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        /// <summary>
        /// Api to create Staffs
        /// </summary>
        /// <param name="name">Name of staff == username in financials db</param>
        /// <param name="staffCode">equivalent to the userID of the staff</param>
        /// <param name="password">password of the stafff</param>
        /// <param name="email">email of the staff. can be null in which case a random email will be used</param>
        /// <returns></returns>
        [HttpPost("CreateStaff")]
        public async Task<ActionResult> PostUser([FromBody] UserAPIModel model)
        {
            string em = GetEmail(model.email);

            var user = new DcProcurement.Staff
            {
                UserName = model.staffCode,
                Email = em,
                CreationDate = DateTime.Now,
                Name = model.name,
                StaffCode = model.staffCode
            };
            var result = await _userManager.CreateAsync(user, model.password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { userId = user.Id, code = code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                //  await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("Staff added in procurement");
            }
            else
            {
                return BadRequest();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet("Login")]
        public async Task<ActionResult> GetLoginUser([FromQuery] string userId, [FromQuery] string password, [FromQuery] string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            //          {
            //              "name": "Abiodun Abdul",
            //"staffCode": "0908",
            //"password": "admin1234",
            //"email": ""

           
            var result = await _signInManager.PasswordSignInAsync(userId, password, true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }
            //if (result.IsLockedOut)
            //{
            //    _logger.LogWarning("User account locked out.");
            //    return RedirectToPage("./Lockout");
            //}
            else
            {
                return BadRequest("Sign In Failed");
            }
            


            // If we got this far, something failed, redisplay form
            // return RedirectToPage("./Lockout");
        }
        /// <summary>
        /// if email is empty, generates a random email else returns the original 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private string GetEmail(string email)
        {
            return string.IsNullOrWhiteSpace(email) ? $"{GenerateRandomNo().ToString()}bsslTest@gmail.com" : email;
        }
    }
}
