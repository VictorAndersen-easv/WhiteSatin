using infra;
using LinqToDB;
using service;

var builder = WebApplication.CreateBuilder(args);

var dbPath = Path.Combine(
    AppContext.BaseDirectory,
    "development.db");

Console.WriteLine($"Database path: {dbPath}");

var connectionString = $"Data Source={dbPath}";

var options = new DataOptions<MyDatabaseConnection>(
    new DataOptions().UseSQLite(connectionString));


builder.Services.AddScoped<MyDatabaseConnection>(_ =>
    new MyDatabaseConnection(options));

builder.Services.AddScoped<LibraryService>();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddCors();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<MyExceptionHandler>();

var app = builder.Build();

// Set up database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyDatabaseConnection>();

    db.CreateTable<Author>(tableOptions: TableOptions.CreateIfNotExists);
    db.CreateTable<Book>(tableOptions: TableOptions.CreateIfNotExists);
    
    // Remove the bad test book
    db.Books
        .Where(b => b.AuthorId != "1")
        .Delete();

    // Seed initial data only if the tables are empty
    if (!db.Authors.Any())
        db.Insert(new Author
        {
            AuthorId = "1",
            AuthorName = "bob"
        });

    if (!db.Books.Any())
        db.Insert(new Book
        {
            BookId = "1",
            BookTitle = "book 1",
            NumberOfPages = 100,
            AuthorId = "1"
        });
}

app.UseExceptionHandler();

app.UseCors(config =>
    config
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        .SetIsOriginAllowed(_ => true));

app.MapControllers();

app.UseOpenApi();
app.UseSwaggerUi();

app.Run();