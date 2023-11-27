using Application.Locations;

namespace API.Controllers;

public class LocationController : BaseApiController
{

    [HttpPost] //api/CreateLocation
    public async Task<IActionResult> CreateLocation(string name)
    {
        return HandleResult(await Mediator.Send(new AddLocation.Command { Name = name }));
    }

    [HttpGet] //api/ GetLocationList
    public async Task<IActionResult> GetLocationList()
    {
        return HandleResult(await Mediator.Send(new LocationList.Query()));
    }

    [HttpGet("{id}")]   //api/GetLocationDetail
    public async Task<IActionResult> GetLocationDetail(int id)
    {
        return HandleResult(await Mediator.Send(new LocationDetail.Query { Id = id }));
    }

    [HttpPut("{id}")]  //api/UpdateLocation
    public async Task<IActionResult>UpdateLocation(int id, BookStoreDto location)
    {
        location.Id = id;
        return HandleResult(await Mediator.Send(new UpdateLocation.Command { Location = location }));
    }

    [HttpDelete("{id}")]  //api/DeleteLocation
    public async Task<IActionResult>DeleteLocation(int id)
    {
        return HandleResult(await Mediator.Send(new DeleteLocation.Command { Id = id }));
    }
}
