using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Models
{
    public class CompanyAddDto
    {
        [Display(Name = "公司名称")]
        [Required(ErrorMessage = "{0}这个字段必填！")]
        [MaxLength(100,ErrorMessage ="{0}的最大长度不可以超过{1}")]
        public string Name { get; set; }

        [Display(Name = "简介")]
        [StringLength(500,MinimumLength =100,ErrorMessage ="{0}的长度范围是：{2}到{1}")]
        public string Introduction { get; set; }
        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto>();

    }
}
