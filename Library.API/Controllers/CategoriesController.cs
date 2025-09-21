using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Library.Domain.Repositories;
using Library.Domain.Entities;
using Library.Application.DTOs;

namespace Library.API.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase {
        private readonly ICategoryRepository _repo;
        public CategoriesController(ICategoryRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var cats = await _repo.ListAllAsync();
            var dtos = cats.Select(c => new CategoryDto { Id = c.Id, Name = c.Name });
            return Ok(dtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id) {
            var cat = await _repo.GetByIdAsync(id);
            if (cat == null) return NotFound();
            return Ok(new CategoryDto { Id = cat.Id, Name = cat.Name });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto) {
            var cat = new Category(dto.Name);
            await _repo.AddAsync(cat);
            dto.Id = cat.Id;
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryDto dto) {
            var cat = await _repo.GetByIdAsync(id);
            if (cat == null) return NotFound();
            cat.UpdateName(dto.Name);
            await _repo.UpdateAsync(cat);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) {
            var cat = await _repo.GetByIdAsync(id);
            if (cat == null) return NotFound();
            await _repo.DeleteAsync(cat);
            return NoContent();
        }
    }
}
