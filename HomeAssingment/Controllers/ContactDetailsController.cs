using HomeAssignment.Models;
using HomeAssignment.Repostories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Reflection;

namespace HomeAssignment.Controllers
{
    public class ContactDetailsController : Controller
    {
        // GET: ContactDetails
        private readonly IRepository _repository;
        public ContactDetailsController(IRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Index(string id)
        {
            var contact = _repository.GetContact(id);
            if (contact is null)
            {
                return NotFound();
            }
            return View(contact);
        }
        [HttpPost]
        public IActionResult SaveEdit(Contact contact)
        {
            var IdChanged = _repository.GetContact(contact.Id) == null;

            if (IdChanged)
            {
                ModelState.AddModelError("Id", "ID cannot be modified");
            }

            if (ModelState.IsValid)
            {
                _repository.UpdateContact(contact);
            }
            else TempData["ErrorMessage"] = "Submit failed, one or more parameters are incorrect";
            return View("../ContactDetails/Index", contact);
        }
    }
}
