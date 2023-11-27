namespace Application.Dtos;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Price { get; set; }
    public string ISBN { get; set; }
    public string SubCategoryName { get; set; }
}
