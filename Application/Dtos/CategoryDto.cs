namespace Application.Dtos;

public class CategoryDto : BookStoreDto
{
    public ICollection<SubCategoryBookListDto> SubCategories { get; set; } = new List<SubCategoryBookListDto>();
}
