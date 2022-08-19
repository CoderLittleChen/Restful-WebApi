using _01.Net_Core_Restful_API.Entities;
using _01.Net_Core_Restful_API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _01.Net_Core_Restful_API.Models
{
    public class EmployeeUpdateDto : EmployeeAddOrUpdateDto
    {
        [Required]
        public override string EmployeeNo { get; set ; }
    }
}
