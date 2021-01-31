using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
   public class NoteDetail
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }// DateTimeOffset is reference type,Reference Types point to an address in memory

        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }// the ?  is referred to as the null-conditional operator,in other words This allows the value type to be null 
        public string  CategoryName { get; set; }
    }
}
