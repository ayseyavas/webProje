using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webProje.Models
{
    public class BookType
    {
        [Key]//@pk (optinal Id yi görüp pk olduğunu anlıyor bp
        public int id { get; set; }
        [Required(ErrorMessage ="Book Type boş bırakılamaz!")]//@notnull
        [MaxLength(25)]
        [DisplayName("Book Type Name")]// "name" değişkeninin html ekranında gözükecek adını belirleme
     
        public string name { get; set; }
    }
}
