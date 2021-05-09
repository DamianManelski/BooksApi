using System;
using System.Collections.Generic;

namespace BooksApi.Infrastructure
{
    public class Book
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Isbn { get; set; }

        public int NumberOfPages { get; set; }

        public DateTime? DeletionDate { get; set; }

        public IList<UserBookOpinion> UsersBookOpinions { get; set; }
    }
}
