namespace Domain;

public class BookCopiesHistoryDto
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int Copies { get; set; }
    public int Price { get; set; }
    public string ActionType { get; set; }
}
