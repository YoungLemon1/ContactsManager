using HomeAssignment.Models;

namespace HomeAssignment.Repostories
{
    public interface IRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact ? GetContact(string id);
        void InsertContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(string id);
        void DeleteAllContacts();
    }
}