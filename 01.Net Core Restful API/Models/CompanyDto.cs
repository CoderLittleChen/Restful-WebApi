using _01.Net_Core_Restful_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Models
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
    }
}
