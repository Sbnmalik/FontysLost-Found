using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class Password
    {
        public int ID { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
