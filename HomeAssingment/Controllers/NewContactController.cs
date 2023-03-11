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

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult CreateContact(Contact contact)
		{
			_repository.InsertContact(contact);
			return RedirectToAction("Index");
		}
	}
}
