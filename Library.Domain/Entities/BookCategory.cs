using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities {
    public class BookCategory {
        public Guid BookId { get; private set; }
        public Book Book { get; private set; } = null!;
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;

        protected BookCategory() { }

        public BookCategory(Book book, Category category) {
            Book = book ?? throw new ArgumentNullException(nameof(book));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            BookId = book.Id;
            CategoryId = category.Id;
        }
    }
}
