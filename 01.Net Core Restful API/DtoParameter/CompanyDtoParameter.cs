using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.DtoParameter
{
    public class CompanyDtoParameter
    {
        //对每页的最大数量进行限制  
        private const int MaxPageSize = 20;

        public string CompanyName { get; set; }
        public string SearchTerm { get; set; }

        public string Fields { get; set; }

        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

    }

}
