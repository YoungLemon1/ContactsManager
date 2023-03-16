﻿using HomeAssignment.Models;
using HomeAssignment.Repostories;
using Microsoft.AspNetCore.Mvc;

namespace HomeAssignment.Controllers
{
	public class NewContactController : Controller
	{
		private readonly IRepository _repository;
		private readonly Contact contact;
		public NewContactController(IRepository repository)
		{
			_repository = repository;
			contact = new Contact();
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
