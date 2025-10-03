using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string EmailAdress {  get; set; }

    }
}
