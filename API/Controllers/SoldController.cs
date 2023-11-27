using Application.SoldDefectiveBooks;
using Microsoft.AspNetCore.Authentication;

namespace API.Controllers
{
    public class SoldController : BaseApiController
    {
        [HttpPost] //api/AddSoldbook
        public async Task<IActionResult> AddSoldBook(SoldBooksDto book)
        {
            return HandleResult(await Mediator.Send(new SoldDefectiveBooks.Command { SoldBooks = book }));
        }
    }
}
