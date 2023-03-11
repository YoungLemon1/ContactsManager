using HomeAssignment.Models;
using System.Data.SqlClient;

namespace HomeAssignment.Repostories
{
    public class Repository : IRepository
    {
        private readonly string connectionString;
        private readonly SqlConnection sqlCon;

        public Repository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            sqlCon = new SqlConnection(connectionString);
        }
        IEnumerable<Contact> IRepository.GetContacts()
        {
            IEnumerable<Contact> contacts = new List<Contact>();
            sqlCon.Open();

            using var command = new SqlCommand("SELECT * FROM Contacts", sqlCon);
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

            sqlCon.Close();
        }

        Contact IRepository.GetContact(int id)
        {
            sqlCon.Open();

            using var command = new SqlCommand($"SELECT * FROM Contacts WHERE Id = {id}", sqlCon);
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
            sqlCon.Close();
            return contact;

        }

        public void InsertContact(Contact contact)
        {
            sqlCon.Open();
            using var command = new SqlCommand("INSERT INTO Contacts (Id, FirstName, LastName, BirthDate, City, Street, HouseNumber, Phone, PhoneAtHome)" +
                $"VALUES ({contact.Id}, {contact.FirstName}, {contact.LastName}, {contact.BirthDate}, {contact.BirthDate}, {contact.Street}, {contact.HouseNumber}, {contact.PhoneAtHome}, {contact.Phone})");
            sqlCon.Close();
        }

        public void UpdateContact(Contact contact)
        {
            sqlCon.Open();
            using var command = new SqlCommand($"UPDATE Contacts " +
                $"SET (id = {contact.Id}, FirstName = {contact.FirstName}, LastName = ${contact.LastName}, BirthDate = {contact.BirthDate}, City = {contact.BirthDate}, Street = {contact.Street}, HouseNumber = {contact.HouseNumber}, PhoneAtHome = {contact.PhoneAtHome}, Phone = {contact.Phone}" +
                $" Contacts WHERE Id = {contact.Id}", sqlCon);
            var reader = command.ExecuteReader();
            sqlCon.Close();
        }

        public void DeleteContact(int id)
        {
            sqlCon.Open();
            using var command = new SqlCommand($"DELETE * FROM Contacts WHERE Id = {id}", sqlCon);
            var reader = command.ExecuteReader();
            sqlCon.Close();
        }
    }
}
