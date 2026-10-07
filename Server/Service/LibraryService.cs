using System.ComponentModel.DataAnnotations;
using infra;
using LinqToDB;

namespace service;

public class LibraryService(MyDatabaseConnection db)
{
    public List<BookDto> GetBooks(int page, int resultsPerPage)
    {
        if (page < 1)
            throw new ValidationException("Page must be 1 or higher");

        if (resultsPerPage < 1)
            throw new ValidationException("Must have 1 or more results per page");

        return db.Books
            .LoadWith(b => b.Author)
            .Take(resultsPerPage)
            .Skip((page - 1) * resultsPerPage)
            .Select(b => new BookDto(b)
            {
                Author = new AuthorDto(b.Author)
            })
            .ToList();

    }


    public BookDto CreateBook(CreateBookRequestDto dto)
    {
        if (dto.NumberOfPages < 1)
        {
            throw new ValidationException("Pages must be 1 or higher");
        }

        var b = new Book()
        {
            NumberOfPages = dto.NumberOfPages,
            BookTitle = dto.BookTitle,
            BookId = Guid.NewGuid().ToString(),
            AuthorId = dto.AuthorId
        };

        db.Insert(b);

        // Load the author from the database
        b.Author = db.Authors
            .First(a => a.AuthorId == b.AuthorId);

        return new BookDto(b);
    }

}