using Hospital.Models;

namespace Hospital.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int HospitalinfoId { get; set; }
        public HospitalInfo HospitalInfo { get; set; }
        public HospitalInfo Hospitalinfo { get; set; }

        // Default constructor
        public RoomViewModel()
        {
        }

        // Parameterized constructor to initialize from a Room model
        public RoomViewModel(Room model)
        {
            Id = model.Id;
            RoomNumber = model.RoomNumber;
            Type = model.Type;
            Status = model.Status;
            HospitalinfoId = model.HospitalId;
            HospitalInfo = model.Hospital;
            
            // Added this to ensure all properties are populated
        }
        public Room ConvertViewModel(RoomViewModel model)
        {
            return new Room
            {
                Id = model.Id,
                RoomNumber = model.RoomNumber,
                Type = model.Type,
                Status = model.Status,
                HospitalId=model.HospitalinfoId,
                Hospital=model.Hospitalinfo
            };
        }
    }
}
