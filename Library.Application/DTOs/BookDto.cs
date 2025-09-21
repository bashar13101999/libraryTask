using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTOs {
    public class BookDto {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Author { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
    }

}
