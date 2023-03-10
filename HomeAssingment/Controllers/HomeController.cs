using HomeAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace HomeAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ActionResult Index()
        {
            List<Contact> contacts = new List<Contact>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using var command = new SqlCommand("SELECT * FROM Contacts", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var contact = new Contact()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        LastName = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        BirthDate = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3),
                        City = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        Street = reader.IsDBNull(5) ? "" : reader.GetString(5),
                        HouseNumber = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                        PhoneAtHome = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        Phone = reader.IsDBNull(8) ? "" : reader.GetString(8)
                    };

                    contacts.Add(contact);
                }
            }

            return View(contacts);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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