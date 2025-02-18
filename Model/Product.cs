using System.ComponentModel.DataAnnotations;

namespace ProductManagementAPI.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public string? Category { get; set; }

        public string? ImageUrl { get; set; }
    }
}
