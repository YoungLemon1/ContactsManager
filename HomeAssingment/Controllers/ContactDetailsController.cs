using HomeAssignment.Models;
using HomeAssignment.Repostories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace HomeAssignment.Controllers
{
    public class ContactDetailsController : Controller
    {
        // GET: ContactDetails
        private readonly IRepository _repository;
        private Contact contact;
        public ContactDetailsController(IRepository repository)
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

        // GET: ContactDetails/Edit/5
        public IActionResult SaveEdit(Contact contact)
        {
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

            if (contact.BirthDate < new DateTime(1900, 1, 1) || contact.BirthDate > DateTime.Now)
            {
                ModelState.AddModelError("BirthDate", "Date out of range");
            }

            if (ModelState.IsValid)
            {
                _repository.UpdateContact(contact);
            }
            else TempData["ErrorMessage"] = "Submit failed, one or more parameters are incorrect";
            return RedirectToAction("Index", new { id = contact.Id });
        }
    }
}
