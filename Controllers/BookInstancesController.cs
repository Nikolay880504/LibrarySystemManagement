using LibrarySystemManagement.Data;
using LibrarySystemManagement.Models.BookInstances;
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

        [HttpGet("instance/listInstances/{id?}")]
        public IActionResult GetListBookInstances(int? id)
        {
            var model = new BookWithInstancesListViewModel();

            if (id.HasValue)
            {
                model.BookViewModel = _bookRepository.GetBookById(id.Value);
                model.BookInstances = _bookInstanceRepository.GetAllBookInstancesForBookId(id.Value).ToList();
            }

            return View(model);
        }

        [HttpGet("instance/form/{bookId}/{instanceId?}")]
        public IActionResult BookInstanceForm(int bookId, int? instanceId)
        {
            var bookInstanceCreationModel = new BookInstanceCreationModel();

            if (bookId > 0)
            {
                var bookViewModel = _bookRepository.GetBookById(bookId);
                if (bookViewModel == null)
                {
                    return NotFound(); 
                }

                bookInstanceCreationModel.BookViewModel = bookViewModel;

                if (instanceId.HasValue && instanceId.Value > 0)
                {
                    var bookInstance = _bookInstanceRepository.Get(instanceId.Value);
                    if (bookInstance == null)
                    {
                        return NotFound(); 
                    }

                    bookInstanceCreationModel.BookInstance = bookInstance;
                }
            }
            else
            {
                return BadRequest("Invalid book ID."); 
            }

            return View(bookInstanceCreationModel);
        }


        [HttpPost("instance/add")]
        public IActionResult AddBookInstance(BookInstance bookInstance)
        {
            if (ModelState.IsValid)
            {
                _bookInstanceRepository.Add(bookInstance);
                return RedirectToAction("GetListBookInstances", new { id = bookInstance.BookId });
            }

            var bookInstanceCreationModel = new BookInstanceCreationModel
            {
                BookViewModel = _bookRepository.GetBookById(bookInstance.BookId)
            };
            return View("BookInstanceForm", bookInstanceCreationModel);
        }
        public IActionResult UpdateBookInstance(BookInstance bookInstance)
        {
            if (bookInstance == null || bookInstance.Id <= 0)
            {
                throw new ArgumentException("Invalid book instance data.");
            }

            var bookInstanceById = _bookInstanceRepository.Get(bookInstance.Id);
            if (bookInstanceById == null)
            {
                throw new KeyNotFoundException("Book instance not found.");
            }

            bookInstanceById.SerialNumber = bookInstance.SerialNumber;
            bookInstanceById.Year = bookInstance.Year;

            _bookInstanceRepository.Update(bookInstanceById);

            return RedirectToAction("GetListBookInstances", new { id = bookInstance.BookId });
        }

        [HttpPost("instance/delete/{bookId}/{instanceId?}")]
        public IActionResult DeleteBookInstance(int bookId, int? instanceId)
        {
            if (instanceId.HasValue)
            {
                _bookInstanceRepository.Delete(instanceId.Value);
            }

            return RedirectToAction("GetListBookInstances", new { id = bookId });
        }
    }
}

