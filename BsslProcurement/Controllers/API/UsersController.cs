using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DcProcurement;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using DcProcurement.Contexts;
using Microsoft.EntityFrameworkCore;
using BsslProcurement.UtilityMethods;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BsslProcurement.Controllers.API
{
    public class UsersAPIModel
    {
        public string name { get; set; }
        public string staffCode { get; set; }
        public string password { get; set; }
        public string email{ get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DcProcurement.Contexts.BSSLSYS_ITF_DEMOContext dEMOContext;
        public UsersController(
            UserManager<User> userManager,
            ILogger<UsersController> logger,
            SignInManager<User> signInManager,BSSLSYS_ITF_DEMOContext _dEMOContext
            )
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            dEMOContext = _dEMOContext;
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
        public async Task<ActionResult> PostUser(UsersAPIModel userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            string em = GetEmail(userData.email);

            var user = new Staff
            {
                UserName = userData.staffCode,
                Email = em,
                CreationDate = DateTime.Now,
                Name = userData.name,
                StaffCode = userData.staffCode
            };

            var result = await _userManager.CreateAsync(user, userData.password);
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
                return BadRequest(result.Errors.ToList());
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
            returnUrl ??= Url.Content("~/");


            var pwd = PasswordEncrypt.GetEncryptedPassword(password);

            var result = await _signInManager.PasswordSignInAsync(userId, pwd, true, lockoutOnFailure: false);

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
        /// Get the rank of the staff
        /// </summary>
        /// <param name="staffCode">the code or id of the staff</param>
        /// <returns></returns>
        [HttpGet("GetStaffRank/{staffCode}")]
        public async Task<ActionResult> GetStaffRank(string staffCode)
        {
            try
            {
                var staff = await dEMOContext.Stafftab.FirstOrDefaultAsync(x => x.Staffid == staffCode);
                if (staff != null)
                {

                    var rank = await dEMOContext.Codestab.FirstOrDefaultAsync(m => m.Option1 == "f4" && m.Code == staff.Positionid);

                    return Ok(rank.Desc1);

                }

                return NotFound($"Staff not found for code {staffCode}");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
