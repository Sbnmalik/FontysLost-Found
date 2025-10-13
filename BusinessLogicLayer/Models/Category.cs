using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

    }
}
