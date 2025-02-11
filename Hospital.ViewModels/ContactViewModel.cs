using Hospital.Models;

namespace Hospital.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; } 
        public string Phone { get; set; } 
        public int HospitalInfoId { get; set; } 

        // Parameterless constructor
        public ContactViewModel()
        {
        }

        // Constructor to map from a Contact model
        public ContactViewModel(Contact model)
        {
            Id = model.Id;
            Email = model.Email;
            Phone = model.Phone;
            HospitalInfoId = model.HospitalId;
        }
        public Contact ConvertViewModel(ContactViewModel model)
        {
            return new Contact
            {
                Email = model.Email,
                Phone = model.Phone,
                HospitalId = model.HospitalInfoId
            };
        }
    }
}
