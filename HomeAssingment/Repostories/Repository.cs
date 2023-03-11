using HomeAssignment.Models;
using System.Data.SqlClient;

namespace HomeAssignment.Repostories
{
    public class Repository : IRepository
    {
        private readonly string connectionString;

        public Repository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        IEnumerable<Contact> IRepository.GetContacts()
        {
            IEnumerable<Contact> contacts = new List<Contact>();
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

                    yield return contact;
                }
            }
        }

        Contact IRepository.GetContact(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using var command = new SqlCommand($"SELECT * FROM Contacts WHERE Id = {id}", connection);
                var reader = command.ExecuteReader();
                var contact = new Contact();
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
                };
                
                return contact;
            }
        }
    }
}
