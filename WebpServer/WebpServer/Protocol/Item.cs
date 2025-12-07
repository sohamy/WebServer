using System.ComponentModel.DataAnnotations;

namespace WebpServer.Protocol
{
    public class AddItemRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000000)]
        public int Price { get; set; }

        [Required]
        [Range(0,Int32.MaxValue)]
        public int Stock { get; set; }
    }
}
