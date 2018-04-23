using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersonalOrganizer.Model {
    public enum Gender { Female = 0, Male = 1 }

    public class Contact {
        [ReadOnly(true), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        public Gender Gender { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress), DisplayFormat(NullDisplayText = "<empty>")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber), DisplayFormat(NullDisplayText = "<empty>")]
        public string Phone { get; set; }
        [DisplayFormat(NullDisplayText = "<empty>")]
        public string Address { get; set; }
        [DisplayFormat(NullDisplayText = "<empty>")]
        public string City { get; set; }
        [DisplayFormat(NullDisplayText = "<empty>")]
        public string State { get; set; }
        [DisplayFormat(NullDisplayText = "<empty>")]
        public string Zip { get; set; }
        public byte[] Photo { get; set; }

        public Contact() { }
        public Contact(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
