using System;
using FluentValidation;

namespace Manager.Struct.DTO.Validations
{
    public class ScheduleDtoValidator : AbstractValidator<ScheduleDto>
    {
        public ScheduleDtoValidator()
        {
            RuleFor(s => s.TimeEnd).Must((start, end) =>
            {
                return DateTimeIsGreater(start.TimeStart, end);
            }).WithMessage("Schedule's End time must be greater than Start time");
        }

        private bool DateTimeIsGreater(DateTime start, DateTime end)
        {
            return end > start;
        }
    }
}