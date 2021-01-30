using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
   public  class CategoryDetails
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }

        [Display(Name = "Created")] // didn't change when displayed?!
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Modified")]// didn't change when displayed?!
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
