using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace webProje.Models
{
    public class ApplicationUser:IdentityUser
    {

        [Required]
        public int StudentNo { get; set; }

        public string? Address { get; set; }

        public string? Major {  get; set; }
       
    }
}
