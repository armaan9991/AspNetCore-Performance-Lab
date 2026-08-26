using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ProductQueryDto
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Category { get; set; }

        public string? Search { get; set; }

        public string? SortBy { get; set; }

        public bool SortDescending { get; set; } = false;
    }
}
