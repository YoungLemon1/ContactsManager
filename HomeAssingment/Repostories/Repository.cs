using HomeAssignment.Models;
using System.Data;
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
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using var command = new SqlCommand("SELECT * FROM Contacts", connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var contact = new Contact();
                    AssignContactFromDb(reader, contact);
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
                ExecuteDataReaderCommand(reader, contact);
                return contact;
            }
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
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using var command = new SqlCommand(
                    "INSERT INTO Contacts (Id, FirstName, LastName, BirthDate, City, Street, HouseNumber, PhoneAtHome, Phone) " +
                    "VALUES (@Id, @FirstName, @LastName, @BirthDate, @City, @Street, @HouseNumber, @PhoneAtHome, @Phone)",
                    connection);

                command.Parameters.Add("@Id", SqlDbType.Int).Value = contact.Id;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = contact.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = contact.LastName;
                command.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = contact.BirthDate;
                command.Parameters.Add("@City", SqlDbType.NVarChar, 20).Value = contact.City;
                command.Parameters.Add("@Street", SqlDbType.NVarChar, 20).Value = contact.Street;
                command.Parameters.Add("@HouseNumber", SqlDbType.Int).Value = contact.HouseNumber;
                command.Parameters.Add("@PhoneAtHome", SqlDbType.NVarChar, 20).Value = contact.PhoneAtHome;
                command.Parameters.Add("@Phone", SqlDbType.NVarChar, 20).Value = contact.Phone;

                int rowsAffected = command.ExecuteNonQuery();
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("UPDATE Contacts SET Id = @Id, FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, City = @City, Street = @Street, HouseNumber = @HouseNumber, PhoneAtHome = @PhoneAtHome, Phone = @Phone WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", contact.Id);
                    command.Parameters.AddWithValue("@FirstName", contact.FirstName);
                    command.Parameters.AddWithValue("@LastName", contact.LastName);
                    command.Parameters.AddWithValue("@BirthDate", contact.BirthDate);
                    command.Parameters.AddWithValue("@City", contact.City ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Street", contact.Street ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@HouseNumber", contact.HouseNumber.HasValue ? contact.HouseNumber.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PhoneAtHome", contact.PhoneAtHome ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", contact.Phone ?? (object)DBNull.Value);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteContact(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using var command = new SqlCommand($"DELETE * FROM Contacts WHERE Id = {id}", connection);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
            }
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
