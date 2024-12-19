using Microsoft.EntityFrameworkCore;

using var context = new TestContext();

context.Books.Add(new Book
{
    Id = 2
});

context.SaveChanges();

var book = context.Books.Select(x =>
    new
    {
        AuthorId = x.Author!.Id.ToString()
    })
    .ToList();

internal class TestContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}

public class Author
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<Book> Books { get; set; } = [];
}

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public Author? Author { get; set; }
}