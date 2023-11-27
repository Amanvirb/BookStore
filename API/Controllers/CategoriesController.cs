namespace API.Controllers;

public class CategoriesController : BaseApiController
{
    [HttpPost] //api/Create Category
    public async Task<IActionResult> CreateCategory(BookStoreNameDto category)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Category = category }));
    }

    [HttpGet] //api/GetCategoryList
    public async Task<IActionResult> GetCategoryList()
    {
        return HandleResult(await Mediator.Send(new List.Query()));
    }


    [HttpGet("{id}")] //api/GetCategoryDetail
    public async Task<IActionResult> GetCategoryDetail(int id)
    {
        return HandleResult(await Mediator.Send(new Detail.Query { Id = id }));
    }

    [HttpPut("{id}")] //api/Update Category
    public async Task<IActionResult> UpdateCategory(int id, BookStoreDto category)
    {
        category.Id = id;
        return HandleResult(await Mediator.Send(new Edit.Command { Category = category }));
    }

    [HttpDelete("{id}")] //api/Delete Category
    public async Task<IActionResult> DeleteCategory(int id)
    {
        return HandleResult(await Mediator.Send(new DeleteCategory.Command { Id = id }));
    }
}
