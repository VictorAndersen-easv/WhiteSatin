using infra;

public class LibraryService(MyDatabaseConnection db)
{
    public List<Book> GetBooks()
    {
       return db.Books.ToList();
    }
}