using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ProductCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Range(0.1,10000)]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;
    }
}
