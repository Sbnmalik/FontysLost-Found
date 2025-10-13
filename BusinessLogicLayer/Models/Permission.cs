using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class Permission
    {
        public int ID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime date_created { get; set; }
    }
}
