namespace Application.Dtos;

public class AddBooksDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Series { get; set; }
    public int Price { get; set; }
    public string ISBN { get; set; }
    public int NumberOfCopies { get; set; }
    public string ActionType { get; set; }
    public string CategoryName { get; set; }
    public string SubCategoryName { get; set; }
    public string Location { get; set; }
}
