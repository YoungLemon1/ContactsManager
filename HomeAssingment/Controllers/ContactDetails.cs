using HomeAssignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace HomeAssignment.Controllers
{
    public class ContactDetailsController : Controller
    {
        // GET: ContactDetails
        private readonly IConfiguration _configuration;
        private Contact contact;
        public ContactDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
            contact = new Contact();
        }
        public ActionResult Index(int id)
        {
            

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using var command = new SqlCommand($"SELECT * FROM Contacts WHERE Id = {id}", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contact.Id = reader.GetInt32(0);
                    contact.FirstName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    contact.LastName = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    contact.BirthDate = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3);
                    contact.City = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    contact.Street = reader.IsDBNull(5) ? "" : reader.GetString(5);
                    contact.HouseNumber = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                    contact.PhoneAtHome = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    contact.Phone = reader.IsDBNull(8) ? "" : reader.GetString(8);
                }
            }
            return View(contact);
        }

        // GET: ContactDetails/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
