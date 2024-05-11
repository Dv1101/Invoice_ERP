using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Invoice_ERP.Models
{
    public class ItemsStock
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public decimal SellPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } // Navigation property

        [Required]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; } // Navigation property

        [Required]
        public int UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        public UnitOfMeasure UnitOfMeasure { get; set; } // Navigation property
       

        [Required]
        public DateTime ManufactureDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string? SKU { get; set; }
        public string? Note { get; set; }
        public decimal TotalEarned { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        public string CreatedBy { get; set; } = String.Empty;
    }
}
