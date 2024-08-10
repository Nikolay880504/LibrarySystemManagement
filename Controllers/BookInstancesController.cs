using LibrarySystemManagement.Data;
using LibrarySystemManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemManagement.Controllers
{
    [Route("api/[controller]")]
    public class BookInstancesController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookInstanceRepository _bookInstanceRepository;
        public BookInstancesController(IBookRepository bookRepository, IBookInstanceRepository instanceRepository)
        {
            _bookRepository = bookRepository;
            _bookInstanceRepository = instanceRepository;
        }
        [HttpGet("bookInstance/form/{id?}")]
        public IActionResult BookInstanceForm(int? id)
        {
            BookInstanceCreationModel bookInstanceCreationModel = new BookInstanceCreationModel();
            if (id.HasValue)
            {
                var book = _bookRepository.Get(id.Value);
                bookInstanceCreationModel.Book = book;
            }


            return View(bookInstanceCreationModel);
        }
        [HttpPost("bookInstance/add")]
        public IActionResult AddBookInstance(BookInstance bookInstance)
        {
            BookInstanceCreationModel bookInstanceCreationModel = new BookInstanceCreationModel();
            if (ModelState.IsValid)
            {
                _bookInstanceRepository.Add(bookInstance);
            }
                
            if (bookInstance.BookId > 0)
            {
                var book = _bookRepository.Get(bookInstance.BookId);
                bookInstanceCreationModel.Book = book;
            }
            return View("BookInstanceForm", bookInstanceCreationModel);
        }
    }
}
