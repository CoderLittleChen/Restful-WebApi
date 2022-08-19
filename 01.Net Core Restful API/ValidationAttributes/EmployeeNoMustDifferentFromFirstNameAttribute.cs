using _01.Net_Core_Restful_API.Models;
using System.ComponentModel.DataAnnotations;

namespace _01.Net_Core_Restful_API.ValidationAttributes
{
    public class EmployeeNoMustDifferentFromFirstNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addDto = (EmployeeAddOrUpdateDto)validationContext.ObjectInstance;
            if (addDto.EmployeeNo == addDto.LastName)
            {
                return new ValidationResult("员工编码不可以等于名", new[] { nameof(EmployeeAddOrUpdateDto) });
            }
            return ValidationResult.Success;
        }
    }
}
