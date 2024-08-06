using FluentValidation;

namespace ShopAppPB301Practice.DTOs.GroupDTOs
{
    public class GroupUpdateDTO
    {
        public string Name { get; set; }
        public int Limit { get; set; }
        public IFormFile File { get; set; }
    }
    public class GroupUpdateDTOValidator : AbstractValidator<GroupUpdateDTO>
    {
        public GroupUpdateDTOValidator()
        {
            RuleFor(g => g.Name)
                .MaximumLength(10)
                .WithMessage("Group name too large")
                .MinimumLength(3)
                .WithMessage("Group name too small");
            RuleFor(g => g.Limit)
                .ExclusiveBetween(2, 10)
                .WithMessage("Range do not meet requirements");
            RuleFor(g => g)
                .Custom((g, context) =>
                {
                    if (g.File != null)
                    {
                        if (g.File.Length / 1024 > 500) context.AddFailure("File", "Size too large");
                        if (!g.File.ContentType.Contains("image/")) context.AddFailure("File can only be image");
                    }
                });
        }
    }
}
