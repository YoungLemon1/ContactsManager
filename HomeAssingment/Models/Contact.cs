using System.ComponentModel.DataAnnotations;

namespace HomeAssignment.Models
{
    public class Contact
    {
        [Key]
        [Required(ErrorMessage = "ID is Required")]
        [RegularExpression("[0-9]{9}", ErrorMessage = "ID should be 9 digits long")]
        public string Id { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression("[A-Za-z]{2,}")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        [RegularExpression("[A-Za-z]{2,}")]
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Range(typeof(DateTime), "1/1/1900", "1/1/2023", ErrorMessage = "Date out of range")]
        public DateTime? BirthDate { get; set; }
        [RegularExpression("[A-Za-z\\s\\-\\']{2,}", ErrorMessage = "City should be at least 2 letters long")]
        public string? City { get; set; }
        [RegularExpression("[A-Za-z\\s\\-\\']{2,}", ErrorMessage = "Street should be at least 2 letters long")]
        public string? Street { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = "House number should be 4 digits long")]
        public int? HouseNumber { get; set; }
        [RegularExpression("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$", ErrorMessage = "Invalid phone number")]
        public string? PhoneAtHome { get; set; }
        [RegularExpression("^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$", ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }
    }
}
