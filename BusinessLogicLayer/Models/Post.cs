using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime date_found { get; set; }

        [DataType(DataType.Date)]
        public DateTime date_created { get; set; }

        public string Description { get; set; }

    }
}
