using System.ComponentModel.DataAnnotations;

namespace Invoice_ERP.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        
        public string ContactPerson { get; set; } = String.Empty;
        [Required]
        public string Email { get; set; } = String.Empty;
        [Required]
        public int PhoneNo { get; set; } 
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public string CreatedBy { get; set; } = String.Empty;
    }
}
