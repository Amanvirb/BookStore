namespace Domain;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<BookCopies> BookCopies { get; set; } = new List<BookCopies>();
}
