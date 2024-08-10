using LibrarySystemManagement.Data;
using LibrarySystemManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace LibrarySystemManagement.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BooksController(IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("listBooks")]
        public IActionResult GetListBooks()
        {
            var listBooks = _bookRepository.GetAllBooks();

            return View(listBooks);
        }

        [HttpGet("book/form/{id?}")]
        public IActionResult BookForm(int? id)
        {
            var listCategories = _categoryRepository.GetAllCategories();
            var viewModel = new BookFormViewModelWithCategories()
            {
                Categories = new SelectList(listCategories, "Id", "Name")
            };

            if (id.HasValue)
            {
                var book = _bookRepository.Get(id.Value);
                if (book != null)
                {
                    viewModel.Form = new BookFormViewModel
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = book.Author,
                        CategoryId = book.CategoryId
                    };
                }
            }

            return View(viewModel);
        }

        [HttpPost("book/new")]
        public IActionResult AddBook(BookFormViewModel form)
        {
            if (ModelState.IsValid)
            {
                var book = new Book()
                {
                    Author = form.Author,
                    Title = form.Title,
                    CategoryId = form.CategoryId
                };
                _bookRepository.Add(book);
                return RedirectToAction("GetListBooks");
            }
            var listCategories = _categoryRepository.GetAllCategories();
            var viewModel = new BookFormViewModelWithCategories
            {
                Form = form,
                Categories = new SelectList(listCategories, "Id", "Name")
            };
            return View("BookForm", viewModel);
        }



        [HttpPost("book/delete/{id}")]
        public IActionResult DeleteBook(int id)
        {
            _bookRepository.Delete(id);
            return RedirectToAction("GetListBooks");
        }

        [HttpPost("book/update")]
        public IActionResult UpdateBook(BookFormViewModel form)
        {
            if (!ModelState.IsValid)
            {
                var listCategories = _categoryRepository.GetAllCategories();
                var viewModel = new BookFormViewModelWithCategories
                {
                    Form = form,
                    Categories = new SelectList(listCategories, "Id", "Name")
                };
                return View("BookForm", viewModel);
            }

            var bookForUpdate = _bookRepository.Get(form.Id);
            if (bookForUpdate != null)
            {
                bookForUpdate.Author = form.Author;
                bookForUpdate.Title = form.Title;
                bookForUpdate.CategoryId = form.CategoryId;
                _bookRepository.Update(bookForUpdate);
            }
            return RedirectToAction("GetListBooks");
        }
    }
}
