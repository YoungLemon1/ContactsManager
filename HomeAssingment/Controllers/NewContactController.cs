using HomeAssignment.Models;
using HomeAssignment.Repostories;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssignment.Controllers
{
	public class NewContactController : Controller
	{
		private readonly IRepository _repository;
		public NewContactController(IRepository repository)
		{
			_repository = repository;
		}

		public IActionResult Index(Contact contact)
		{
            return View(contact);
		}

        [HttpPost]
		public IActionResult CreateContact(Contact contact)
		{
			var contactExsists = _repository.GetContact(contact.Id) != null;

            if (contactExsists)
            {
                ModelState.AddModelError("Id", "Id already Exists");
            }

            if (ModelState.IsValid)
            {
                _repository.InsertContact(contact);
                return Redirect("/");
            }
            TempData["ErrorMessage"] = "Submit failed, one or more parameters are incorrect";
            return RedirectToAction("Index", contact);         
        }
	}
}
