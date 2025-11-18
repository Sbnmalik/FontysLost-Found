namespace BusinessLogicLayer.Models
{

    public class Post
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public int? CategoryId { get; set; }
        public byte[]? Attachment { get; set; }

        public int? FinderId { get; set; }
        public int? RetrieverId { get; set; }
    }
}
