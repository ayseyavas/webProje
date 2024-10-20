using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using webProje.Models;
using webProje.Utility;

namespace webProje.Controllers
{
    public class RentController : Controller
    {
        // private readonly AppDBContext _appDBContext;//container kapsamında olan bir eleman olduğunu belli etmek için _ kullanılıyor

        private readonly IRentRepository _rentRepository;
        private readonly IBookRepository _bookRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public RentController(IRentRepository _rentRepository, IBookRepository _bookRepository, IWebHostEnvironment _webHostEnvironment)
        {
            this._webHostEnvironment = _webHostEnvironment;
            this._bookRepository = _bookRepository;///////////
            this._rentRepository = _rentRepository;
        }
        public IActionResult Index()
        {

            //            List<Book> book = _bookRepository.GetAll().ToList();
            List<Rent> rent = _rentRepository.GetAll(includeProps: "Book").ToList();


            return View(rent);
        }


        //GET
        public IActionResult AddUpdateRent(int? id)
        {
            IEnumerable<SelectListItem> booklist = _bookRepository.GetAll()//ovshit
                 .Select(k => new SelectListItem
                 {
                     Text = k.Title,
                     Value = k.Id.ToString()

                 });

            var defaultItem = new SelectListItem
            {
                Value = "", // Seçim yapılmadığında bu değer olacak
                Text = "Lütfen Kitap Seçiniz"
            };

            var finallist = new List<SelectListItem> { defaultItem };
            finallist.AddRange(booklist);




            ViewBag.booklist = finallist; //controllerdan viewa veri ektarımını sağlar tersi çalışmaz

            if (id == null || id == 0)
            {
                //Add
                return View();
            }
            else
            {
                //Update
                Rent? rent = _rentRepository.Get(x => x.Id == id); //Expression<Func<T, bool>> expression 
                if (rent == null)
                {
                    return NotFound();
                }
                return View(rent);
            }

        }

        //Post
        [HttpPost]
        public IActionResult AddUpdateRent(Rent rent)
        {

            if (ModelState.IsValid)
            {

                if (rent.Id == 0)
                {
                    _rentRepository.Add(rent);
                    TempData["Success"] = "New rent success";
                }
                else
                {
                    _rentRepository.Update(rent);
                    TempData["Success"] = "Rent updated";
                }

                _rentRepository.Save();

                return RedirectToAction("Index", "Rent");//eğer view kullanılmak istenilirse actionun kendisine ait cshtml i oluşturulmalıdır
            }

            foreach (var state in ModelState.Values)
            {
                foreach (var error in state.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }


            return View();
        }



        public IActionResult DeleteRent(int? id)
        {
            // id ve name parametrelerine göre işlemler yapılacak

            IEnumerable<SelectListItem> booklist = _bookRepository.GetAll()//ovshit
                .Select(k => new SelectListItem
                { 
                    Text = k.Title,
                    Value = k.Id.ToString()

                });
            ViewBag.booklist = booklist;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Rent? rent = _rentRepository.Get(x => x.Id == id);
            if (rent == null)
            {
                return NotFound();
            }
            return View(rent);
        }


        [HttpPost, ActionName("DeleteRent")]
        public IActionResult DeleteRentPOST(int? id)
        {

            Rent?rent = _rentRepository.Get(x => x.Id == id);
            if (rent == null)
            {
                return NotFound();
            }

            _rentRepository.Delete(rent);
            _rentRepository.Save();

            TempData["Success"] = "Delete method";
            return RedirectToAction("Index", "Rent");
        }

    }
}
