using Hospital.Utility;
using Hospital.ViewModels;

namespace Hospital.Services
{
    public interface IContactService
    {
        PagedResult<ContactViewModel> GetAll(int pageNumber, int PageSize);
        ContactViewModel GetHospitalById(int ContactId);
        void UpdateHospitalInfo(ContactViewModel contact);
        void InsertHospitalInfo(ContactViewModel contact);
        void DeleteHospitalInfo(int id);
    }
}
