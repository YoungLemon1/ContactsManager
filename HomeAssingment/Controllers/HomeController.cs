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

        public void DeleteMarked(List<int> ids)
        {
            foreach (int id in ids)
            {
                _repository.DeleteContact(id);
            }
        }

        //private string SetTableColumnInt(SqlDataReader ? reader, int column)
        //{
        //    if (reader.GetInt32(column) is null)
        //        return "";
        //    return reader.GetInt32(column).ToString();
        //}
        //private string SetTableColumnString(SqlDataReader ? reader, int column)
        //{
        //    if (reader.GetString(column) == null)
        //        return "";
        //    return reader.GetString(column).ToString();
        //}

        //private string SetTableColumnDateTime(SqlDataReader? reader, int column)
        //{

        //}
    }
}