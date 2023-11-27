namespace Application.Dtos;

public class BookCopiesDto
{
    public string BookTitle { get; set; }
    public string LocationName { get; set; }
    public int TotalCopies { get; set; }

    public ICollection<BookCopiesHistoryDto> BookCopiesHistory { get; set; } = new List<BookCopiesHistoryDto>();

}
