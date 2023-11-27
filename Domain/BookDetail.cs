namespace Domain;

public class BookDetail
{
    public int Id { get; set; }
    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
    public string Title { get; set; }
    public int Price { get; set; }
    public string ISBN { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public int? SeriesId { get; set; }
    public Series Series { get; set; }
    public ICollection<BookCopies> BookCopies { get; set; } = new List<BookCopies>();
}
