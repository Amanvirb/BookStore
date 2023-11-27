namespace Application.Dtos;

public class SubCategoryBookListDto : BookStoreDto
{

    public ICollection<BookDetailDto> Books { get; set; } = new List<BookDetailDto>();
}
