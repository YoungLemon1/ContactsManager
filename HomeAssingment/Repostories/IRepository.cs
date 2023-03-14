using HomeAssignment.Models;

namespace HomeAssignment.Repostories
{
    public interface IRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(int id);
        void InsertContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(int id);
        void DeleteAllContacts();
    }
}