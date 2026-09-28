using infra;
using Microsoft.AspNetCore.Mvc;

public class LibraryController(LibraryService service) : ControllerBase
{
    [HttpGet(nameof(GetBooks))]
    public List<Book> GetBooks()
    {
        return service.GetBooks();
    }
}