using BookRepo.Models;
using BookRepo.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class booksController : ControllerBase
    {
        private readonly IBook book;

        public booksController(IBook book)
        {
            this.book = book;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() { 
            return Ok( new { res= await book.GetAll() });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            return Ok(new { res = await book.Delete(id) });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(new { res = await book.GetById(id) });
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Book entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedBook = await book.Add(entity);
                return CreatedAtAction(nameof(GetById), new { id = addedBook.id }, new { res = addedBook });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                entity.id = id; 
                var updatedBook = await book.Update(id, entity);
                return Ok(new { res = updatedBook });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = $"Book with ID {id} not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }



    }
}
