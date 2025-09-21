using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Library.Application.Interfaces;
using Library.Application.Commands;

namespace Library.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase {
        private readonly IBookService _svc;
        public BooksController(IBookService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var result = await _svc.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("with-categories")]
        public async Task<IActionResult> GetAllWithCategories() {
            var result = await _svc.GetAllWithCategoriesUsingStoredProcAsync();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id) {
            var res = await _svc.GetByIdAsync(id);
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command) {
            var created = await _svc.CreateAsync(command);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBookCommand command) {
            await _svc.UpdateAsync(id, command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) {
            await _svc.DeleteAsync(id);
            return NoContent();
        }
    }
}
