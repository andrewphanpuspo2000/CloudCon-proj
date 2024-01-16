using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1.Controller;
[ApiController]
[Route("[controller]")]
public class ReviewController:ControllerBase
{
 public readonly ReviewServices _reviewServices;
 public ReviewController(ReviewServices borrowBookService)
 {
  this._reviewServices = borrowBookService;
 }

 [HttpPost]
 public async Task<IActionResult> AddComment([FromBody] Review comment)
 {
  var result = await _reviewServices.AddCommentService(comment);
  return Ok(result);
 }

 [HttpDelete("{id}")]
 public async Task<IActionResult> DeleteComment(string id)
 {
  var result = await _reviewServices.DeleteCommentServices(id);
  return Ok(result);
 }

}