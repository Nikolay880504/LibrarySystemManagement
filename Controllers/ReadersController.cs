using LibrarySystemManagement.Data;
using LibrarySystemManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemManagement.Controllers
{
    [Route("api/[controller]")]
    public class ReadersController : Controller
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IBookRepository _bookRepository;

        public ReadersController(IReaderRepository readerRepository, IBorrowingRepository borrowingRepository, IBookRepository bookRepository)
        {
            _readerRepository = readerRepository;
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet("listReaders")]
        public IActionResult GetListReaders()
        {
            var allReaders = _readerRepository.GetAllReaders();
            return View(allReaders);
        }

        [HttpGet("reader/form/{id?}")]
        public IActionResult ReaderForm(int id)
        {
            if (id != 0)
            {
                var reader = _readerRepository.Get(id);
                return View(reader);
            }
            return View();
        }

        [HttpPost("reader/delete/{id}")]
        public IActionResult DeleteReader(int id)
        {
            if (id != 0)
            {
                _readerRepository.Delete(id);
            }
            return RedirectToAction("GetListReaders");
        }

        [HttpPost("reader/add")]
        public ActionResult AddReader(Reader reader)
        {

            if (ModelState.IsValid)
            {
                _readerRepository.Add(reader);
                return RedirectToAction("GetListReaders");
            }
            return View("ReaderForm", reader);
        }

        [HttpPost("reader/update")]
        public ActionResult UpdateReader(Reader reader)
        {
            if (!ModelState.IsValid)
            {
                return View("ReaderForm", reader);
            }
            var readerForUpdate = _readerRepository.Get(reader.ID);
            if (readerForUpdate != null)
            {
                readerForUpdate.ID = reader.ID;
                readerForUpdate.Name = reader.Name;
                readerForUpdate.Email = reader.Email;
                readerForUpdate.RegistrationDate = reader.RegistrationDate;
                _readerRepository.Update(readerForUpdate);

            }
            return RedirectToAction("GetListReaders");
        }

        [HttpGet("reader/cart/{id}")]
        public ActionResult ReaderCart(int id)
        {
            var allBooksForReader = _borrowingRepository.GetAllBorrowingBooksByReaderId(id);
            var borrowingViewModelList = new BorrowedBooksViewModel
            {
                BorrowedBooks = allBooksForReader.ToList(),
                ReaderId = id
            };
            return View(borrowingViewModelList);
        }


    }
}
