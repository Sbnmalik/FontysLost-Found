using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Models
{
    public class Permission
    {
        public int ID { get; set; }
        public string Title {  get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
