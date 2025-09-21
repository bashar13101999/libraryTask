using Library.Application.Commands;
using Library.Application.DTOs;
using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Domain.Repositories;

namespace Library.Application.Services {
    public class BookService : IBookService{
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILibraryReadStoredProc _storedProcReader;


        public BookService(IBookRepository bookRepository, ICategoryRepository categoryRepository, ILibraryReadStoredProc storedProcReader) {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _storedProcReader = storedProcReader;
        }


        public async Task<BookDto> CreateAsync(CreateBookCommand command) {
            var book = new Book(command.Title, command.Author);

            foreach (var catId in command.CategoryIds.Distinct()) {
                var cat = await _categoryRepository.GetByIdAsync(catId);
                if (cat != null) book.AddCategory(cat);
            }

            await _bookRepository.AddAsync(book);

            return MapToDto(book);
        }

        public async Task DeleteAsync(Guid id) {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new InvalidOperationException("Book not found");
            await _bookRepository.DeleteAsync(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync() {
            var books = await _bookRepository.ListAllAsync();
            return books.Select(MapToDto);
        }

        public async Task<IEnumerable<BookDto>> GetAllWithCategoriesUsingStoredProcAsync() {
            return await _storedProcReader.GetAllBooksWithCategoriesAsync();
        }


        public async Task<BookDto?> GetByIdAsync(Guid id) {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;
            return MapToDto(book);
        }

        public async Task UpdateAsync(Guid id, UpdateBookCommand command) {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new InvalidOperationException("Book not found");

            book.Update(command.Title, command.Author);

            var incoming = command.CategoryIds.Distinct().ToHashSet();

            var toRemove = book.Categories.Select(c => c.CategoryId).Where(cid => !incoming.Contains(cid)).ToList();
            foreach (var cid in toRemove) book.RemoveCategory(cid);

            foreach (var cid in incoming) {
                if (!book.Categories.Any(c => c.CategoryId == cid)) {
                    var cat = await _categoryRepository.GetByIdAsync(cid);
                    if (cat != null) book.AddCategory(cat);
                }
            }

            await _bookRepository.UpdateAsync(book);
        }

        private static BookDto MapToDto(Book b) {
            return new BookDto {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Categories = b.Categories.Select(c => new CategoryDto { Id = c.Category.Id, Name = c.Category.Name }).ToList()
            };
        }

    }
}
