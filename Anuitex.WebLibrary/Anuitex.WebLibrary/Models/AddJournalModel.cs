using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Anuitex.WebLibrary.Models
{
    public class AddJournalModel : BaseModel
    {
        public int Id { get; set; }
        public int? PhotoId { get; set; }
        public string PhotoPath { get; set; }

        [Required(ErrorMessage = "Field must be filled")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public string Periodicity { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public string Subjects { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Field must be filled")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Price must be greater zero")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Field must be filled")]        
        public double Price { get; set; }        
        public bool IsEdit { get; set; }
    }
}