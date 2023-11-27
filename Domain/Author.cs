namespace Domain;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DOB { get; set; }
    public string Country { get; set; }
    public ICollection<BookDetail> Books { get; set; } = new List<BookDetail>();
}
