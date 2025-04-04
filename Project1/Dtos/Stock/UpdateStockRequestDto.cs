using System.ComponentModel.DataAnnotations;

namespace Project1.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "Company name cannot be over 50 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 10000000000)]
        public decimal Price { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Industry cannot be over 50 characters")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 50000000000)]
        public long MarketCap { get; set; }
    }
}
