using _01.Net_Core_Restful_API.Entities;
using _01.Net_Core_Restful_API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _01.Net_Core_Restful_API.Models
{
    [EmployeeNoMustDifferentFromFirstName]
    public abstract class EmployeeAddOrUpdateDto : IValidatableObject
    {
        [Required]
        public abstract string EmployeeNo { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == LastName)
            {
                yield return new ValidationResult("姓和名不能一样", new[] { nameof(FirstName), nameof(LastName) });
            }
        }


    }
}
