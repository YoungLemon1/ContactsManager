namespace HomeAssignment.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly ? BirthDate { get; set; }
        public string ? City { get; set; }
        public string ? Street { get; set; }
        public int ? HouseNumber { get; set; }
        public string ? PhoneAtHome { get; set; }
        public string ? Phone { get; set; }
    }
}
