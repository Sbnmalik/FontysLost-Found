using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs
{
    // Incoming data
    public class postCreateDTO
    {
        //add data annotations for validation
        [Required, StringLength(20)]
        public string Title { get; set; }
        [Required, StringLength(200)]
        public string Description { get; set; } = "";
        //optional file from the form
        public byte[]? Attachment { get; set; }

    }
    // Outgoing data
    public class postDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
