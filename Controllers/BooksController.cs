using LibrarySystemManagement.Data;
using LibrarySystemManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemManagement.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("listBooks")]
        public IActionResult GetListBooks()
        {
            IEnumerable<Book> listBooks = _bookRepository.GetAllBooks();
            return View(listBooks);
        }

        [HttpGet("book/form/{id?}")]
        public IActionResult BookForm(int id)
        {
            if(id > 0)
            {
                var book = _bookRepository.Get(id);
                return View(book);
            }
                return View();
        }

        [HttpPost("newBook")]
        public IActionResult AddBook(Book book)
        {
            {
                if (ModelState.IsValid)
                {
                    _bookRepository.Add(book);
                    return RedirectToAction("GetListBooks");
                }
                return View("BookForm", book);
            }
        }

        [HttpPost("books/delete/{id}")]
        public IActionResult DeleteBook(int id)
        {          
            _bookRepository.Delete(id); 
            return RedirectToAction("GetListBooks");
        }

        [HttpPost("books/update")]
        public IActionResult UpdateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View("BookForm", book);
            }
            var bookForUpdate = _bookRepository.Get(book.ID);
            if (bookForUpdate != null)
            {
                bookForUpdate.ID = book.ID;
                bookForUpdate.Author = book.Author;
                bookForUpdate.Title = book.Title;
                bookForUpdate.Year = book.Year;
                bookForUpdate.Category = book.Category;
                _bookRepository.Update(bookForUpdate);              
            }
            return RedirectToAction("GetListBooks");
        }
    }
}
