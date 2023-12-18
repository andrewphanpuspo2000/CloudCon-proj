using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1.Controller;
[ApiController]
[Route("[controller]")]
public class BorrowBookController:ControllerBase
{
    private readonly BorrowBookService _borrowBookService;

    public BorrowBookController(BorrowBookService borrowBookService)
    {
        this._borrowBookService = borrowBookService;
    }
    [HttpPost("borrowbook")]
    public async Task<IActionResult> BorrowBook(BorrowBook borrow)
    {
        var result = await _borrowBookService.AddBorrowBook(borrow);
        return Ok(result);
    }

    [HttpDelete("deleteBorrowBook/{id}/{bookid}")]
    public async Task<IActionResult> ReturnBorrow(string id,string bookid)
    {
        var result = await _borrowBookService.ReturnBookServices(id,bookid);
        return Ok(result);
    }
}