using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands {
    public class UpdateBookCommand {
        public string Title { get; set; } = null!;
        public string? Author { get; set; }
        public List<Guid> CategoryIds { get; set; } = new();
    }
}
