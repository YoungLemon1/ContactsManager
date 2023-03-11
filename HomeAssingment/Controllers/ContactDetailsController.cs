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
        private bool editing;
        public ContactDetailsController(IRepository repository)
        {
            _repository = repository;
            contact = new Contact();
            editing = false;
        }
        public ActionResult Index(int id)
        {
            contact = _repository.GetContact(id);
            ViewBag.Editing = editing;
            return View(contact);
        }

        public ActionResult SetEditMode()
        {
            editing = !editing;
            ViewBag.Editing = editing;
            return View(contact);
        }

        // GET: ContactDetails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContactDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ContactDetails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContactDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
