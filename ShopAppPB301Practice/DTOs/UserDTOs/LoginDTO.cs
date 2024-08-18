using FluentValidation;

namespace ShopAppPB301Practice.DTOs.UserDTOs
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
    public class LogintDTOValidator : AbstractValidator<LoginDTO>
    {
        public LogintDTOValidator()
        {
            RuleFor(l => l.Username)
                .NotEmpty()
                .WithMessage("Username field required");
        }
    }
}
