using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
   public  class CategoryCreate
    {
        [Required]
        [MinLength(4,ErrorMessage ="Please enter at least 4 characters.")]
        [MaxLength(15,ErrorMessage ="There are tooo many caracters in this field, 15 characters is the maximum.")]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
