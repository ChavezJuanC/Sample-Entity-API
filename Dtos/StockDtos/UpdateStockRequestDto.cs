using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateStockRequestDto
    {
        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "Symbol must be between 1 and 10 characters.")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "CompanyName must be between 1 and 10 characters.")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000000, ErrorMessage = "Purchase must be between 1 and 1,000,000,000.")]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100, ErrorMessage = "LastDiv must be between 0.001 and 100.")]
        public decimal LastDiv { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Industry must be between 1 and 25 characters.")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000000000000, ErrorMessage = "MarketCap must be between 1 and 1,000,000,000,000.")]
        public long MarketCap { get; set; }

    }

}

