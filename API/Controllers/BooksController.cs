namespace API.Controllers
{
    public class BooksController : BaseApiController
    {
        [HttpPost]  //api/Addbook
        public async Task<IActionResult> AddBook(AddBooksDto book)
        {
            return HandleResult(await Mediator.Send(new AddBook.Command { BookDetail = book} ));
        }

        [HttpGet]  //api/GetBooks
        public async Task<IActionResult> GetBooks([FromQuery] ProductParams productParams)
        {
            return HandlePagedResult(await Mediator.Send(new BookList.Query {Params = productParams}));
        }

        [HttpGet("{id}")]   //Get//GetBookDetail
        public async Task<IActionResult> GetBookDetail(int id)
        {
            return HandleResult(await Mediator.Send(new ShowBookDetail.Query { Id = id}));
        }

        [HttpDelete("{id}")] //DeleteBook
        public async Task<IActionResult> DeleteBook(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteBook.Command { Id = id }));
        }

        [HttpPut] //Updatebookdetail
        public async Task<IActionResult> UpdateBook(UpdateBookDto bookDetail)
        {
            return HandleResult(await Mediator.Send(new UpdateBook.Command { BookDetail = bookDetail }));
        }
    }
}
