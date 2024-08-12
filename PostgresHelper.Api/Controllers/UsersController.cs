using Microsoft.AspNetCore.Mvc;
using PostgresHelper.DataAccess.Entities;
using PostgresHelper.PostgresNuget;

namespace PostgresHelper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("{id:int}")]
    public ActionResult Get(int id)
    {
        return Ok();
    }
    
    [HttpPost]
    public ActionResult Post(User user)
    {
        return Ok();
    }
    
    [HttpGet("all")]
    public ActionResult<PagingModel<User>> GetAll()
    {
        return Ok();
    }
}