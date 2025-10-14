using fotoservice.api.data.interfaces;

namespace api.Controllers;

public class UserController : BaseApiController
{
  private readonly IUsers _users;

  public UserController(IUsers users)
  {
    _users = users;

  }
  [HttpGet("getAllUsers")]
  public async Task<IActionResult> allUsers() { return Ok(await _users.GetUsers()); }

  [HttpDelete("RemoveUser/{id}")]
  public async Task<IActionResult> deleteUser(int id)
  {
    var result = await _users.Delete(id);
    if (await _users.SaveChangesAsync()) { return Ok("User removed"); }
    return BadRequest("User was not deleted");
  }

  [HttpPut("UpdateUser")]
  public async Task<IActionResult> updateUser(UserForUpdateDto us)
  {
   var result = await _users.Update(us);
   return Ok("User updated");
  }

  [HttpGet("SpecificUser/{id}")]
  public async Task<IActionResult> getUser(int id){
  
    return Ok(await _users.GetUser(id));
  }


}