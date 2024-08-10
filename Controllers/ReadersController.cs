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
            var allReaders = _readerRepository.GetAllReaders().ToList();
            return View(allReaders);
        }

        [HttpGet("reader/form/{id?}")]
        public IActionResult ReaderForm(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var reader = _readerRepository.Get(id.Value);
                if (reader != null)
                {
                    return View(reader);
                }
                return NotFound();
            }
            return View();
        }

        [HttpPost("reader/delete/{id}")]
        public IActionResult DeleteReader(int id)
        {
            if (id > 0)
            {
                var reader = _readerRepository.Get(id);
                if (reader != null)
                {
                    _readerRepository.Delete(id);
                }
                else
                {
                    return NotFound(); 
                }
            }
            return RedirectToAction("GetListReaders");
        }

        [HttpPost("reader/add")]
        public IActionResult AddReader(Reader reader)
        {
            if (ModelState.IsValid)
            {
                _readerRepository.Add(reader);
                return RedirectToAction("GetListReaders");
            }
            return View("ReaderForm", reader); 
        }

        [HttpPost("reader/update")]
        public IActionResult UpdateReader(Reader reader)
        {
            if (ModelState.IsValid)
            {
                var existingReader = _readerRepository.Get(reader.ID);
                if (existingReader != null)
                {
                    existingReader.Name = reader.Name;
                    existingReader.Email = reader.Email;
                    existingReader.RegistrationDate = reader.RegistrationDate;
                    _readerRepository.Update(existingReader);
                    return RedirectToAction("GetListReaders");
                }
                return NotFound(); 
            }
            return View("ReaderForm", reader);
        }

        [HttpGet("reader/cart/{id}")]
        public IActionResult ReaderCart(int id)
        {
            var borrowedBooks = _borrowingRepository.GetAllBorrowingBooksByReaderId(id).ToList();
            var reader =  _readerRepository.Get(id);
            
            var viewModel = new BorrowedBooksViewModel
            {
                BorrowedBooks = borrowedBooks,
                Reader = reader
            };
            return View(viewModel);
        }
    }
}

