using System;
using FluentValidation;

namespace Manager.Struct.DTO.Validations
{
    public class ActivityDtoValidator : AbstractValidator<ActivityDto>
    {
        public ActivityDtoValidator()
        {
            RuleFor(s => s.TimeEnd).Must((start, end) =>
            {
                return DateTimeIsGreater(start.TimeStart, end);
            }).WithMessage("Task's End time must be greater than Start time");
        }

        private bool DateTimeIsGreater(DateTime start, DateTime end)
        {
            return end > start;
        }
    }
}