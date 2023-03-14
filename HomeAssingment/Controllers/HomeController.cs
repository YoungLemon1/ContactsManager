using HomeAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using HomeAssignment.Repostories;

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
        public void DeleteMarked([FromBody] List<int> ids)
        {
            foreach (int id in ids)
            {
                _repository.DeleteContact(id);
            }
        }
    }
}