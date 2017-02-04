using System.ComponentModel.DataAnnotations;

namespace Nackademiska.Models
{
    public class LoginInformation
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}