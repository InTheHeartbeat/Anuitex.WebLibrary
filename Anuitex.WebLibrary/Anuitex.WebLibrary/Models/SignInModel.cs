using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Anuitex.WebLibrary.Models
{
    public class SignInModel : BaseModel
    {
        [Required(ErrorMessage = "Field must be filled")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Length must be between 3 and 50 characters")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Field must be filled")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Length must be between 4 and 50 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
    }
}