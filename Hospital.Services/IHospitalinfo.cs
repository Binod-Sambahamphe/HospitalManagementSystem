using Hospital.ViewModels;
using System.Linq.Dynamic.Core;

namespace Hospital.Services
{
    public interface IHospitalinfo
    {
        PagedResult<HospitalInfoViewModel> GetAll(int pageNumber,int PageSize);
        HospitalInfoViewModel GetHospitalById(int id);
        void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo);
        void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo);
        void DeleteHospitalInfo(int id);
    }
}
