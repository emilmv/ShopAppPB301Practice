namespace ShopAppPB301Practice.DTOs.BookDTOs
{
    public class BookReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
        public List<AuthorInBookReturnDTO> BookAuthors { get; set; }
    }
    public class AuthorInBookReturnDTO
    {
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
    }
}
