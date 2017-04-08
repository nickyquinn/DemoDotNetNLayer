using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using DemoApp.Services;
using DemoApp.Services.DTO;

namespace DemoApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private IPersonnelService _service;

        public HomeController(IPersonnelService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_service.GetAll());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PersonDto model)
        {
            if (ModelState.IsValid)
            {
                _service.AddPerson(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var person = _service.GetAll().Where(x => x.PersonDtoId == id).FirstOrDefault();
            if (person != null)
            {
                _service.RemovePerson(person);
            }
            return RedirectToAction("Index");
        }
    }
}