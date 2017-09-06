using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Anuitex.WebLibrary.Data;

namespace Anuitex.WebLibrary.Models
{
    public class AddBookModel : BaseModel
    {
        [Required(ErrorMessage = "Field must be filled")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public int Pages { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price must be greater zero")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        [Range(typeof(double), "0,10", "999999999,99", ErrorMessage = "Price must be greater zero")]
        public double Price { get; set; }
        public int? PhotoId { get; set; }

        public string PhotoPath { get; set; }
        public bool IsEdit { get; set; }
        public int Id { get; set; }
    }
}