using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DcProcurement
{
    public class User : IdentityUser
    {
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; } = DateTime.Now;
    }
}
