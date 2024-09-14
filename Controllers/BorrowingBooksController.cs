
using LibrarySystemManagement.Data;
using LibrarySystemManagement.Models.BookInstances;
using LibrarySystemManagement.Models.Borrowers;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemManagement.Controllers
{
    [Route("api/[controller]")]
    public class BorrowingBooksController : Controller
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookInstanceRepository _bookInstanceRepository;

        public BorrowingBooksController(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IBookInstanceRepository bookInstanceRepository)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _bookInstanceRepository = bookInstanceRepository;
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
            var instancesForRents = new List<BookInstancesForRent>();
            var books = _bookRepository.GetBooksWithCategories().ToList();
            
            foreach (var book in books)
            {
                var bookInstancesForRent = new BookInstancesForRent
                {
                    Model = book,
                    BookInstances = _bookInstanceRepository.GetAllBookInstancesForBookId(book.Id),
                    ReaderId = id
                };
                instancesForRents.Add(bookInstancesForRent);
            }

            return View(instancesForRents);
        }

        [HttpPost("book/{bookInstanceId}/reader/{readerId}")]
        public IActionResult RentBookInstanceUserById(int bookInstanceId, int readerId)
        {
            var bookInstance = _bookInstanceRepository.Get(bookInstanceId);
            if (bookInstance == null)
            {
                ModelState.AddModelError("", "Book instance not found.");
                return RedirectToAction("BorrowBooksForm", new { id = readerId });
            }

            var borrowingBook = new BorrowingBook
            {
                BookInstanceId = bookInstanceId,
                ReaderId = readerId,
                BorrowDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddMonths(1)
            };

            _borrowingRepository.Add(borrowingBook);

            return RedirectToAction("ReaderCart", "Readers", new { id = readerId });
        }

        [HttpPost("updateReturnDate/reader/{readerId}/book/{bookId}")]
        public IActionResult UpdateActualReturnDateBook(int readerId, int bookId)
        {
            var borrowBook = _borrowingRepository.Get(bookId);
            if (borrowBook != null)
            {
                borrowBook.ActualReturnDate = DateTime.Now;
                _borrowingRepository.Update(borrowBook);
            }
            else
            {
                ModelState.AddModelError("", "Borrowing record not found.");
            }

            return RedirectToAction("ReaderCart", "Readers", new { id = readerId });
        }
    }
}
