using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Category
    {
        public int ID {  get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
