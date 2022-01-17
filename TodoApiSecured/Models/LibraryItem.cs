using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Models
{
    public class LibraryItem
    {
        [Key]   //assume identity
        [Column(TypeName = "int")]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Display(Name = "Autor")]
        public string Author { get; set; }

        [StringLength(150)]
        [Column(TypeName = "nvarchar(150)")]
        [Display(Name = "Wydanie, Rok")]
        public string Edition { get; set; }

        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Display(Name = "Wypożyczone przez")]
        public string CheckedOutByWhom { get; set; }

        [Column(TypeName = "int")]
        [Display(Name = "Kondycja ksiązki skala 1-5")]
        public int Condition { get; set; }

        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        [Display(Name = "Krótki Opis")]
        public string Description { get; set; }
    }
}
