using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs;

public class ProductUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(0.1, 10000)]
    public decimal Price { get; set; }
    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;
}