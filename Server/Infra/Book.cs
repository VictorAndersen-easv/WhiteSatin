using LinqToDB.Mapping;

namespace infra;

public class Book
{
    [PrimaryKey]
    public string BookId { get; set; } = null!;

    public string BookTitle { get; set; } = null!;

    public int NumberOfPages { get; set; }

    public string AuthorId { get; set; } = null!;

    [Association(
        ThisKey = nameof(AuthorId),
        OtherKey = nameof(Author.AuthorId))]
    public Author Author { get; set; } = null!;
}


public class Author
{
    [PrimaryKey]
    public string AuthorId { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    [Association(
        ThisKey = nameof(AuthorId),
        OtherKey = nameof(Book.AuthorId))]
    public List<Book> BooksWrittenByAuthor { get; set; } = new();
}
