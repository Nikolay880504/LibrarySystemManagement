using LibrarySystemManagement.Data;
using LibrarySystemManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystemManagement.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("category/form/{id?}")]
        public IActionResult CategoryForm(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var category = _categoryRepository.Get(id.Value);
                if (category != null)
                {
                    return View(category);
                }
                return NotFound();
            }
            return View();
        }

        [HttpPost("category/add")]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                return RedirectToAction("GetListCategories");
            }
            return View("CategoryForm", category); 
        }

        [HttpPost("category/delete/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            if (id > 0)
            {
                var category = _categoryRepository.Get(id);
                if (category != null)
                {
                    _categoryRepository.Delete(id);
                }
                else
                {
                    return NotFound(); 
                }
            }
            return RedirectToAction("GetListCategories");
        }

        [HttpGet]
        public IActionResult GetListCategories()
        {
            var allCategories = _categoryRepository.GetAllCategories();
            return View(allCategories);
        }

        [HttpPost("category/update")]
        public IActionResult UpdateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var categoryForUpdate = _categoryRepository.Get(category.Id);
                if (categoryForUpdate != null)
                {
                    categoryForUpdate.Name = category.Name;
                    _categoryRepository.Update(categoryForUpdate);
                    return RedirectToAction("GetListCategories");
                }
                return NotFound(); 
            }
            return View("CategoryForm", category);
        }
    }
}

