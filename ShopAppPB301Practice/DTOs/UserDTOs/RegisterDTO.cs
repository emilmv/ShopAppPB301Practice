using FluentValidation;

namespace ShopAppPB301Practice.DTOs.UserDTOs
{
    public class RegisterDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }

    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(r => r.FullName)
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .MaximumLength(30)
                .WithMessage("Name is too large")
                .MinimumLength(6)
                .WithMessage("Name is too short");
            RuleFor(r => r.Username)
                .NotEmpty()
                .WithMessage("Username cannot be empty")
                .MaximumLength(30)
                .WithMessage("Username is too large")
                .MinimumLength(6)
                .WithMessage("Username is too short");
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Enter valid email address");
            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Field can not be empty")
                .MinimumLength(6)
                .WithMessage("Password is too short")
                .MaximumLength(20)
                .Equal(r => r.RepeatPassword)
                .WithMessage("Passwords do not match");

        }
    }
}
