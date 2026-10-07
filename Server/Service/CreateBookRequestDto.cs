using Facet;
using infra;

[Facet(typeof(Book), [nameof(Book.Author), nameof(Book.BookId)])]
public partial class CreateBookRequestDto;
