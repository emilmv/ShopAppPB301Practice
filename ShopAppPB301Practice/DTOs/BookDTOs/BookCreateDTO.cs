using FluentValidation;

namespace ShopAppPB301Practice.DTOs.BookDTOs
{
    public class BookCreateDTO
    {
        public string Name { get; set; }
        public int PageCount { get; set; }
        public int[] AuthorIds { get; set; }
    }
    public class BookCreateDTOValidator : AbstractValidator<BookCreateDTO>
    {
        public BookCreateDTOValidator()
        {
            RuleFor(b => b.Name)
                .NotNull()
                .MaximumLength(100)
                .MinimumLength(2);
            RuleFor(b => b.PageCount)
                .NotEmpty()
                .InclusiveBetween(1, 1000);
            RuleFor(b => b.AuthorIds)
                .NotEmpty();
        }
    }
}
