using FluentValidation;

namespace ShopAppPB301Practice.DTOs.UserDTOs
{
    public class ResetPasswordDTO
    {
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
    public class ResetPasswordDTOValidator : AbstractValidator<ResetPasswordDTO>
    {
        public ResetPasswordDTOValidator()
        {
            RuleFor(r => r.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(30)
                .Equal(r => r.RepeatPassword);
        }
    }
}
