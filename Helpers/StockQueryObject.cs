using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class StockQueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? Name { get; set; } = null;
        public decimal? Price { get; set; } = null;
        public decimal? LastDiv { get; set; } = null;
        public string? Industry { get; set; } = null;
        public long? MarketCap { get; set; } = null;

        public bool IsDescending { get; set; } = false;
        public string? SortBy { get; set; } = null;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        
    }
}