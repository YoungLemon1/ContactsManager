using System.ComponentModel.DataAnnotations;

namespace HomeAssignment.Models
{
    public class Contact
    {
        [Key]
        [Required(ErrorMessage = "ID is Required")]
        public string Id { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ? BirthDate { get; set; }
        public string ? City { get; set; }
        public string ? Street { get; set; }
        public int ? HouseNumber { get; set; }
        public string ? PhoneAtHome { get; set; }
        public string ? Phone { get; set; }
    }
}
