using infra;
using LinqToDB;
using LinqToDB.Data;

public class MyDatabaseConnection(DataOptions<MyDatabaseConnection> options) : DataConnection(options.Options)
{
    public ITable<Book> Books => this.GetTable<Book>();
    public ITable<Author> Authors => this.GetTable<Author>();
}