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

            _repository.UpdateContact(contact);
            return RedirectToAction("Index", contact);
        }
    }
}
