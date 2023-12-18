using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1.Controller;
[ApiController]
[Route("[controller]")]
public class BooksController:ControllerBase
{
    private readonly BooksServices _booksServices;
    public BooksController(BooksServices booksServices)
    {
        
        _booksServices = booksServices;
    }

    [HttpPost]
    public async Task<IActionResult> AddBookControl([FromBody] Books book)
    {
      var result= await _booksServices.AddBook(book);
       return Ok(result);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(string id,[FromBody] Books books)
    {
        var result = await _booksServices.UpdateBoook(id, books);
        return Ok(result);
    }
   
}