
using LibrarySystemManagement.Data;
using LibrarySystemManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemManagement.Controllers
{
    [Route("api/[controller]")]
    public class BorrowingBooksController : Controller
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IBookRepository _bookRepository;

        public BorrowingBooksController(IBorrowingRepository borrowingRepository, IBookRepository bookRepository)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet("listBorrowingBooks")]
        public IActionResult GetListBorrowings()
        {
            var allBorrowingBooks = _borrowingRepository.GetAllBorrowingBooks().ToList();
            return View(allBorrowingBooks);
        }

        [HttpPost("borrowBook/delete/book/{bookId}/reader/{readerId}")]
        public IActionResult DeleteBorrowingBook(int bookId, int readerId)
        {
            _borrowingRepository.Delete(bookId);
            return RedirectToAction("ReaderCart", "Readers", new { id = readerId });
        }

        [HttpGet("borrow_book/{id}")]
        public IActionResult BorrowBooksForm(int id)
        {
            var books = _bookRepository.GetAllBooks().ToList();
            var borrowingViewModel = new FreeBooksViewModel
            {
                ReaderId = id,
                Books = books
            };
            return View(borrowingViewModel);
        }

        [HttpPost("book/{bookId}/reader/{readerId}")]
        public IActionResult RentBookUserById(int bookId, int readerId)
        {
            var book = _bookRepository.Get(bookId);
            if (book == null)
            {
                ModelState.AddModelError("", "Book not found.");
                return RedirectToAction("BorrowBooksForm", new { id = readerId });
            }

            var borrowingBook = new BorrowingBook
            {
                BookID = bookId,
                ReaderID = readerId,
                BorrowDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddMonths(1)
            };

            _borrowingRepository.Add(borrowingBook);

            return RedirectToAction("ReaderCart", "Readers", new { id = readerId });
        }
    }
}

