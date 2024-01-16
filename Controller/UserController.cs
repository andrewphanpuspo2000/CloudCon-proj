using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1.Controller;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase
{
    private readonly UserServices _userServices;

    public UserController(UserServices
        userServices)
    {
        _userServices = userServices;
    }

    [HttpPost]
    public async Task<IActionResult> addUser([FromBody]User user)
    {
        Console.WriteLine("Executed");
             await _userServices.CreateUserAsync(user);
         return Ok(new {status="success"});
    }

    [HttpGet]
    public async  Task<List<User>> getUser()
    {
        var data = await _userServices.GetUserAsync();
        return data;
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> updateUser(string id,[FromBody]User user)
    {
        if (user == null)
        {
            return BadRequest("Invalid user object.");
        }
       var result= await _userServices.updateUserAsync(id,user);
        return Ok(result);

    }
}