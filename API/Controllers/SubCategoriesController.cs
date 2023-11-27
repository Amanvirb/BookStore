using Application.SubCategories;

namespace API.Controllers;

public class SubCategoriesController : BaseApiController
{
    [HttpPost] //api/CreateCategory
    public async Task<IActionResult> CreateCategory(SubCategoryDto category)
    {
        return HandleResult(await Mediator.Send(new CreateSubCategory.Command { Category = category }));
    }

    [HttpGet] //api/GetSubCategoryList
    public async Task<IActionResult> GetSubCategoryList()
    {
        return HandleResult(await Mediator.Send(new SubCategoryList.Query()));
    }

    [HttpGet("{id}")] //api/GetSubCategoryDetail
    public async Task<IActionResult> GetSubCategoryDetail(int id)
    {
        return HandleResult(await Mediator.Send(new SubCategoryDetail.Query { Id = id}));
    }

    [HttpPut("{id}")] //api/UpdateSubCategory
    public async Task<IActionResult> UpdateCategory(int id, BookStoreDto subCategory)
    {
        subCategory.Id = id;
        return HandleResult(await Mediator.Send(new SubCategoryEdit.Command { SubCategory = subCategory }));
    }

    [HttpDelete("{id}")] //api/DeleteSubCategpry
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        return HandleResult(await Mediator.Send(new DeleteSubCategory.Command { Id = id }));
    }

}
