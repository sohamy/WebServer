using System.ComponentModel.DataAnnotations;

namespace WebpServer.Protocol
{
    public class CreateUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nickname { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }
    }
}
