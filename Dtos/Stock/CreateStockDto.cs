using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockDto
    {
        [Required]
        [MaxLength(5, ErrorMessage = "Symbol must be at most 10 characters long")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(0, 1000000000000, ErrorMessage = "Price must be between 0 and 1000000000000")]
        public decimal Price { get; set; }
        [Required]
        [Range(0.001, 100, ErrorMessage = "Last Dividend must be between 0.001 and 100")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Industry must be at most 50 characters long")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(0, 5000000000000, ErrorMessage = "Market Cap must be between 0 and 1000000000000")]
        public long MarketCap { get; set; }
    }
}