using HomeAssignment.Models;
using HomeAssignment.Repostories;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssignment.Controllers
{
	public class NewContactController : Controller
	{
		private readonly IRepository _repository;
		private Contact contact;
		public NewContactController(IRepository repository)
		{
			_repository = repository;
			contact = new Contact();
		}

		public IActionResult Index(string id)
		{
            var c = _repository.GetContact(id);
            if (c != null)
            {
                contact = c;
            }
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

			if (contact.Id.Length != 9)
			{
                ModelState.AddModelError("Id", "Id must be 9 digits long");
            }

			if (string.IsNullOrEmpty(contact.FirstName))
			{
				ModelState.AddModelError("FirstName", "First Name is required");
			}

            if (string.IsNullOrEmpty(contact.LastName))
            {
                ModelState.AddModelError("LastName", "Last Name is required");
            }

            if (contact.FirstName.Length < 2)
            {
                ModelState.AddModelError("FirstName", "First Name must be at least 2 letters long");
            }

            if (contact.LastName.Length < 2)
            {
                ModelState.AddModelError("LastName", "Last Name must be at least 2 letters long");
            }

            if (contact.City != null && contact.City.Length < 2 )
            {
                ModelState.AddModelError("City", "E");
            }

            if (contact.BirthDate < new DateTime(1900, 1, 1) || contact.BirthDate > DateTime.Now)
            {
                ModelState.AddModelError("BirthDate", "Date out of range");
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
