﻿namespace ShopAppDll.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }

        public Author()
        {
            BookAuthors = new();
        }
    }
}
