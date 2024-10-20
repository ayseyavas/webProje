using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using webProje.Models;
using webProje.Utility;

namespace webProje.Controllers
{
    // Authorizetion bütün controllerda isteniyor ise burda oalcak. Fonks bazlı ise fonksiyonuun üstüne yazılacak

    public class BookTypeController : Controller
    {
        // private readonly AppDBContext _appDBContext;//container kapsamında olan bir eleman olduğunu belli etmek için _ kullanılıyor

        private readonly IBookTypeRepository _bookTypeRepository;
        public BookTypeController(IBookTypeRepository _bookTypeRepository)
        {

            this._bookTypeRepository = _bookTypeRepository;///////////

        }
        public IActionResult Index()
        {

            List<BookType> bookTypes = _bookTypeRepository.GetAll().ToList();
            return View(bookTypes);
        }


        //GET
        public IActionResult AddNewType()
        {
            return View();
        }

        //Post
        [HttpPost]
        public IActionResult AddNewType(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Add(bookType);
                _bookTypeRepository.Save();
                TempData["Success"] = "Add method";
                return RedirectToAction("Index");//eğer view kullanılmak istenilirse actionun kendisine ait cshtml i oluşturulmalıdır
            }
            return View();
        }

        public IActionResult UpdateType(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookType? bookType = _bookTypeRepository.Get(x => x.id == id); //Expression<Func<T, bool>> expression 
            if (bookType == null)
            {
                return NotFound();
            }
            return View(bookType);
        }


        [HttpPost]
        public IActionResult UpdateType(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Update(bookType);
                _bookTypeRepository.Save();

                TempData["Success"] = "Update method";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult DeleteType(int? id, string name)
        {
            // id ve name parametrelerine göre işlemler yapılacak
       
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookType? bookType = _bookTypeRepository.Get(x=>x.id == id);
            if (bookType == null)
            {
                return NotFound();
            }
            return View(bookType);
        }


        [HttpPost, ActionName("DeleteType")]
        public IActionResult DeleteTypePOST(int? id )
        {

            BookType? bookType = _bookTypeRepository.Get(x => x.id == id);
            if (bookType == null)
            {
                return NotFound();
            }

            _bookTypeRepository.Delete(bookType);
            _bookTypeRepository.Save();
    
            TempData["Success"] = "Delete method";
            return RedirectToAction("Index", "BookType");
        }

    }
}
