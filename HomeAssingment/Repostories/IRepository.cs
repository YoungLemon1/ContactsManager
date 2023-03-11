using HomeAssignment.Models;

namespace HomeAssignment.Repostories
{
    public interface IRepository
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(int id);
    }
}