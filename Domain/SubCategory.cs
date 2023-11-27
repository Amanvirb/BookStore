namespace Domain;

public class SubCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<BookDetail> Books { get; set; } = new List<BookDetail>();
}
