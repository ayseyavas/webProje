using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webProje.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        [Range(10, 5000)]
        public double Price { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }


        [ValidateNever]
        public int BookTypeId { get; set; }

        [ForeignKey("BookTypeId")]

        [ValidateNever]
        public BookType BookType { get; set; }

        [ValidateNever]
        [DisplayName("Picture Url")]
        public string PicUrl { get; set; }

    }
}
