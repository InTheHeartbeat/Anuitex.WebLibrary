using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anuitex.WebLibrary.Models
{
    public class SignUpModel : BaseModel
    {
        [Remote("CheckLoginAvailable", "Account")]
        [Required(ErrorMessage = "Field must be filled")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be between 3 and 50 characters")]        
        public string Login { get; set; }

        [Required(ErrorMessage = "Field must be filled")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Length must be between 4 and 50 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field must be filled")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Length must be between 4 and 50 characters")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords do not equal")]
        public string RepeatPassword { get; set; }
    }
}