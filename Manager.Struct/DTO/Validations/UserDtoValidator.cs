using FluentValidation;

namespace Manager.Struct.DTO.Validations
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(user => user.Profession).NotEmpty().WithMessage("Profession cannot be empty");
            RuleFor(user => user.Avatar).NotEmpty().WithMessage("Profession cannot be empty");
        }
    }
}
