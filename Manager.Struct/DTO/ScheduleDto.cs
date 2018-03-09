using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Manager.Struct.DTO.Validations;

namespace Manager.Struct.DTO
{
    public class ScheduleDto : IValidatableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Creator { get; set; }
        public int CreatorId { get; set; }
        public int[] Attendees { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new ScheduleDtoValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => 
                new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
