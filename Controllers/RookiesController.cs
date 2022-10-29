using Microsoft.AspNetCore.Mvc;
using MVCAssigmentDay2.Models;
using MVCAssigmentDay2.Services;
namespace MVCAssigmentDay2.Controllers
{
    public class RookiesController : Controller
    {
        private readonly ILogger<RookiesController> _logger;
        private readonly IPersonServices _personService;

        public RookiesController(ILogger<RookiesController> logger, IPersonServices personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public IActionResult Index()
        {
            var models = _personService.GetAll();
            return View(models);
        }

        public IActionResult Detail(int index)
        {
            var person = _personService.GetOne(index);

            if (person != null)
            {
                var model = new PersonDetailModel
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Gender = person.Gender,
                    DateOfBirth = person.DateOfBirth,
                    BirthPlace = person.BirthPlace,
                    PhoneNumber = person.PhoneNumber,
                };
                ViewData["Index"] = index;
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PersonCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var person = new PersonModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    BirthPlace = model.BirthPlace,
                    PhoneNumber = model.PhoneNumber,
                    IsGraduated = false
                };
                _personService.Create(person);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int index)
        {
            var person = _personService.GetOne(index);

            if (person != null)
            {
                var model = new PersonUpdateModel
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    BirthPlace = person.BirthPlace,
                    PhoneNumber = person.PhoneNumber,
                };
                ViewData["Index"] = index;
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Update(int index, PersonUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var person = _personService.GetOne(index);

                if (person != null)
                {
                    person.FirstName = model.FirstName;
                    person.LastName = model.LastName;
                    person.PhoneNumber = model.PhoneNumber;
                    person.BirthPlace = model.BirthPlace;

                    _personService.Update(index, person);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int index)
        {
            var result = _personService.Delete(index);
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteAndRedirectToResultView(int index)
        {
            var result = _personService.Delete(index);
            if (result == null)
            {
                return NotFound();
            }
            // TempData["DeletePersonName"] = result.FullName;
            HttpContext.Session.SetString("DeletePersonName", result.FullName);
            return View("DeleteResult");
        }

        public IActionResult DeleteResult()
        {
            // ViewBag.DeletePersonName = HttpContext.Session.GetString("DeletePersonName");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}