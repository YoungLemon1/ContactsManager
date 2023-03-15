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
        public IEnumerable<Contact> GetContacts()
        {
            using var connection = new SqlConnection(connectionString);
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
        

        public Contact ? GetContact(string id)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand($"SELECT * FROM Contacts WHERE Id = {id}", connection);
            var reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                return null;
            }
            var contact = new Contact();
            ExecuteDataReaderCommand(reader, contact);
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
            using var connection = new SqlConnection(connectionString);

            using var command = new SqlCommand(
                "INSERT INTO Contacts (Id, FirstName, LastName, BirthDate, City, Street, HouseNumber, PhoneAtHome, Phone) " +
                "VALUES (@Id, @FirstName, @LastName, @BirthDate, @City, @Street, @HouseNumber, @PhoneAtHome, @Phone)",
                connection);

            AssignParamsToCommand(contact, command);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void UpdateContact(Contact contact)
        {
            using var connection = new SqlConnection(connectionString);
            using (var command = new SqlCommand("UPDATE Contacts SET Id = @Id, FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, City = @City, Street = @Street, HouseNumber = @HouseNumber, PhoneAtHome = @PhoneAtHome, Phone = @Phone WHERE Id = @Id", connection))
            {
                AssignParamsToCommand(contact, command);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteContact(string id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using var command = new SqlCommand($"DELETE FROM Contacts WHERE Id = {id}", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAllContacts()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using var command = new SqlCommand($"DELETE FROM Contacts", connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static void AssignParamsToCommand(Contact contact, SqlCommand command)
        {
            command.Parameters.AddWithValue("@Id", contact.Id).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@FirstName", contact.FirstName).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@LastName", contact.LastName).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@BirthDate", contact.BirthDate ?? (object)DBNull.Value).SqlDbType = SqlDbType.DateTime;
            command.Parameters.AddWithValue("@City", contact.City ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@Street", contact.Street ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@HouseNumber", contact.HouseNumber ?? (object)DBNull.Value).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@PhoneAtHome", contact.PhoneAtHome ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@Phone", contact.Phone ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
        }

        private static void AssignContactFromDb(SqlDataReader dataReader, Contact contact)
        {
            contact.Id = dataReader.GetString(0).Trim();
            contact.FirstName = dataReader.IsDBNull(1) ? "" : dataReader.GetString(1).Trim();
            contact.LastName = dataReader.IsDBNull(2) ? "" : dataReader.GetString(2).Trim();
            contact.BirthDate = dataReader.IsDBNull(3) ? null : dataReader.GetDateTime(3);
            contact.City = dataReader.IsDBNull(4) ? "" : dataReader.GetString(4).Trim();
            contact.Street = dataReader.IsDBNull(5) ? "" : dataReader.GetString(5).Trim();
            contact.HouseNumber = dataReader.IsDBNull(6) ? null : dataReader.GetInt32(6);
            contact.PhoneAtHome = dataReader.IsDBNull(7) ? "" : dataReader.GetString(7).Trim();
            contact.Phone = dataReader.IsDBNull(8) ? "" : dataReader.GetString(8).Trim();  
        }
    }
}
