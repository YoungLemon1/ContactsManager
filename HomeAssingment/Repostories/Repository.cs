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
                var contact = new Contact();
                AssignContactFromDb(reader, contact);
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
            ExecuteDataReaderCommand(reader, contact);
            sqlCon.Close();
            return contact;

        }

        private static void ExecuteDataReaderCommand(SqlDataReader reader, Contact contact)
        {
            while (reader.Read())
            {
                AssignContactFromDb(reader, contact);
            }
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

        private static void AssignContactFromDb(SqlDataReader dataReader, Contact contact)
        {
            contact.Id = dataReader.GetInt32(0);
            contact.FirstName = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1).Trim();
            contact.LastName = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2).Trim();
            contact.BirthDate = dataReader.IsDBNull(3) ? DateTime.MinValue : dataReader.GetDateTime(3);
            contact.City = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4).Trim();
            contact.Street = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5).Trim();
            contact.HouseNumber = dataReader.IsDBNull(6) ? 0 : dataReader.GetInt32(6);
            contact.PhoneAtHome = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7).Trim();
            contact.Phone = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8).Trim();  
        }
    }
}
