using HomeAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HomeAssignment.Repostories;
using System.Text.Json;

namespace HomeAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var contacts = _repository.GetContacts().ToList();
            return View(contacts);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public void DeleteMarked([FromBody] List<string> ids)
        {
            foreach (string id in ids)
            {
                _repository.DeleteContact(id);
            }
        }

        public void DeleteAllContacts()
        {
            _repository.DeleteAllContacts();
            RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SavePhone([FromBody] JsonElement model)
        {
            if (!model.TryGetProperty("id", out var idElement) || idElement.ValueKind != JsonValueKind.Number)
                return BadRequest(new { success = false, message = "Invalid or missing ID" });

            string id = idElement.GetInt32().ToString();

            if (!model.TryGetProperty("phone", out var phoneElement) || phoneElement.ValueKind != JsonValueKind.String)
                return BadRequest(new { success = false, message = "Invalid or missing phone number" });

            string phone = phoneElement.GetString()!;

            var contact = _repository.GetContact(id.ToString());
            if (contact == null)
                return NotFound(new { success = false, message = "Contact not found" });

            contact.Phone = phone;

            var IdChanged = _repository.GetContact(id) == null;

            if (IdChanged)
            {
                ModelState.AddModelError("Id", "ID cannot be modified");
            }

            if (ModelState.IsValid)
            {
                _repository.UpdateContact(contact);
                return Ok(new { success = true });
            }
 
            else
            {
                // Log the exception
                return StatusCode(500, new { success = false, message = "An error occurred while updating the contact" });
            }
        }
    }
}