using FluentValidation;

namespace ShopAppPB301Practice.DTOs.StudentDTOs
{
    public class StudentCreateDTO
    {
        public string Name { get; set; }
        public double Point { get; set; }
        public int GroupId { get; set; }
    }
    public class StudentCreateDTOValidator : AbstractValidator<StudentCreateDTO>
    {
        public StudentCreateDTOValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(1);
            RuleFor(s => s.Point)
                .NotEmpty()
                .InclusiveBetween(0, 100);
            RuleFor(s => s.GroupId)
                .NotEmpty();
        }
    }
}
