using Facet;
using infra;

namespace service;

[Facet(typeof(Author), nameof(Author.BooksWrittenByAuthor))]
public partial class AuthorDto;

[Facet(typeof(Book), nameof(Book.Author))]
public partial class BookDto
{
    public AuthorDto Author { get; set; }
}