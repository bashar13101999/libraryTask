using Library.Application.Commands;
using Library.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces {
    public interface IBookService {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<IEnumerable<BookDto>> GetAllWithCategoriesUsingStoredProcAsync();
        Task<BookDto?> GetByIdAsync(Guid id);
        Task<BookDto> CreateAsync(CreateBookCommand command);
        Task UpdateAsync(Guid id, UpdateBookCommand command);
        Task DeleteAsync(Guid id);
    }
}
