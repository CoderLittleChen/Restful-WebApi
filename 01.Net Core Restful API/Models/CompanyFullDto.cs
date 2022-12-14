using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Models
{
    public class CompanyFullDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Country { get; set; }
        public string Industry { get; set; }
        public string Product { get; set; }
        public string Introduction { get; set; }
    }
}
