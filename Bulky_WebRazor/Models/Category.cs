using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bulky_WebRazor.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30, ErrorMessage = "Please enter Category Name Less than 30")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Display Order")]
        [Range(1, 250, ErrorMessage = "Please enter a number between 1 and 250.")]
        public int DisplayOrder { get; set; }

    }
}
