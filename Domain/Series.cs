namespace Domain;

public class Series
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<BookDetail> Books { get; set; } = new List<BookDetail>();

}
