using Library.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Interfaces {
    public interface ILibraryReadStoredProc {
        Task<IEnumerable<BookDto>> GetAllBooksWithCategoriesAsync();
    }
}
