namespace Domain;

public class BookCopiesHistory
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int Copies { get; set; }
    public int Price { get; set; }
    public ActionTypeEnum ActionType { get; set; }
}
