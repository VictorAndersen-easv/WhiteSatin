using System.Reflection.Metadata.Ecma335;
using Facet;
using infra;


[Facet(sourceType:typeof(Author),exclude: nameof(Author.BooksWrittenByAuthor))]
public partial class AuthorDto;

[Facet(sourceType: typeof(Book),exclude: nameof(Book.Author))]
public partial class BookDto
{
    public AuthorDto Author { get; set; }
}
