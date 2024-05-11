using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Invoice_ERP.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Description { get; set; } = String.Empty;
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public string CreatedBy { get; set; } = String.Empty;
    }
}
