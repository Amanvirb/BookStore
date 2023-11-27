namespace Application.Dtos;

public class LocationDto : BookStoreDto
{
    public ICollection<BookCopiesDto> BookCopies { get; set; } = new List<BookCopiesDto>();
    public ICollection<BookCopiesHistoryDto> CopiesHistory { get; set; } = new List<BookCopiesHistoryDto>();

}
