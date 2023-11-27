namespace Application.Dtos;

public class BookDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Series { get; set; }
    public int Price { get; set; }
    public string ISBN { get; set; }
    public DateTime DateTime { get; set; }
    public string CategoryName { get; set; }
    public string SubCategoryName { get; set; }

    public ICollection<BookCopiesDto> BookCopies { get; set; } = new List<BookCopiesDto>();

}
