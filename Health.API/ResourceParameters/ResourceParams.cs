using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.ResourceParameters
{
    public class ResourceParams
    {
        const int maxPageSize = 20;
        private int _pageSize = 5;

        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
