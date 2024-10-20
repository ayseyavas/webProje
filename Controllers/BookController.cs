using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using webProje.Models;
using webProje.Utility;

namespace webProje.Controllers
{

    public class BookController : Controller
    {
        // private readonly AppDBContext _appDBContext;//container kapsamında olan bir eleman olduğunu belli etmek için _ kullanılıyor

        private readonly IBookRepository _bookRepository;
        private readonly IBookTypeRepository _bookTypeRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IBookRepository _bookRepository, IBookTypeRepository _bookTypeRepository, IWebHostEnvironment _webHostEnvironment)
        {
            this._webHostEnvironment = _webHostEnvironment;
            this._bookRepository = _bookRepository;///////////
            this._bookTypeRepository = _bookTypeRepository;

        }


        public IActionResult Index()
        {

//            List<Book> book = _bookRepository.GetAll().ToList();
            List<Book> book = _bookRepository.GetAll(includeProps:"BookType").ToList();


            return View(book);
        }



        //GET
        public IActionResult AddUpdateBook(int? id)
        {
            IEnumerable<SelectListItem> bookTypelist = _bookTypeRepository.GetAll()//ovshit
                 .Select(k => new SelectListItem
                 {
                     Text = k.name,
                     Value = k.id.ToString()

                 });

            var defaultItem = new SelectListItem
            {
                Value = "", // Seçim yapılmadığında bu değer olacak
                Text = "Lütfen Tür Seçiniz"
            };

            var finallist = new List<SelectListItem> { defaultItem };
            finallist.AddRange(bookTypelist);




            ViewBag.booktypelist = finallist; //controllerdan viewa veri ektarımını sağlar tersi çalışmaz

            if (id == null || id == 0)
            {
                //Add
                return View();
            }
            else
            {
                //Update
                Book? book = _bookRepository.Get(x => x.Id == id); //Expression<Func<T, bool>> expression 
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }

        }



        //Post
        [HttpPost]
        public IActionResult AddUpdateBook(Book book, IFormFile? file)
        {

            var errors = ModelState.Values.SelectMany(x => x.Errors);
            if (ModelState.IsValid)
            {
                
                if(file!=null){string wwwRootPath = _webHostEnvironment.WebRootPath;
                string bookPath = Path.Combine(wwwRootPath, @"img");


                using (var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);//ovvshit
                }

                    book.PicUrl = @"\img\" + file.FileName;
                }


                if (book.Id == null)
                {
                    _bookRepository.Add(book);
                    TempData["Success"] = "Add method";
                }
                else
                {
                    _bookRepository.Update(book);
                    TempData["Success"] = "Update method";
                }

                _bookRepository.Save();

                return RedirectToAction("Index");//eğer view kullanılmak istenilirse actionun kendisine ait cshtml i oluşturulmalıdır
            }
            return View();
        }

        //public IActionResult UpdateBook(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //Book? book = _bookRepository.Get(x => x.Id == id); //Expression<Func<T, bool>> expression 
        //    //if (book == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //return View(book);
        //}


        //[HttpPost]
        //public IActionResult UpdateBook(Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _bookRepository.Update(book);
        //        _bookRepository.Save();

        //        TempData["Success"] = "Update method";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}



        public IActionResult DeleteBook(int? id)
        {
            // id ve name parametrelerine göre işlemler yapılacak

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? book = _bookRepository.Get(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }



        [HttpPost, ActionName("DeleteBook")]
        public IActionResult DeleteBookPOST(int? id)
        {

            Book? book = _bookRepository.Get(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.Delete(book);
            _bookRepository.Save();

            TempData["Success"] = "Delete method";
            return RedirectToAction("Index", "Book");
        }

    }
}
