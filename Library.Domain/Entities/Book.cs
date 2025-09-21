using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities {
    public class Book {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;
        public string? Author { get; private set; }

        private readonly List<BookCategory> _categories = new();
        public IReadOnlyCollection<BookCategory> Categories => _categories.AsReadOnly();

        protected Book() { }

        public Book(string title, string? author = null, DateTime? publishedAt = null) {
            Id = Guid.NewGuid();
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentNullException(nameof(title));
            Author = author;
        }

        public void Update(string title, string? author) {
            if (!string.IsNullOrWhiteSpace(title)) Title = title;
            Author = author;
        }

        public void AddCategory(Category category) {
            if (category == null) throw new ArgumentNullException(nameof(category));
            if (!_categories.Any(c => c.CategoryId == category.Id))
                _categories.Add(new BookCategory(this, category));
        }

        public void RemoveCategory(Guid categoryId) {
            var existing = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (existing != null) _categories.Remove(existing);
        }
    }
}
