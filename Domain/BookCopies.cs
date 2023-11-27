namespace Domain;

public class BookCopies
{
    public int BookDetailId { get; set; }
    public BookDetail BookDetail { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public ICollection<BookCopiesHistory> BookCopiesHistory { get; set; }
}
