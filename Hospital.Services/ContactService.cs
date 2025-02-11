using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Utility;
using Hospital.ViewModels;

namespace Hospital.Services
{
    public class ContactService : IContactService
    {
        private IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteHospitalInfo(int id)
        {
            
            var vm = _unitOfWork.GenericRepository<Contact>().GetById(id);
            _unitOfWork.GenericRepository<Contact>().Delete(vm);
            _unitOfWork.Save();
        }

        public PagedResult<ContactViewModel> GetAll(int pageNumber, int PageSize)
        {
            var vm = new ContactViewModel();
            int totalCount;
            List<ContactViewModel> vmList = new List<ContactViewModel>();

            try
            {
                int excludeRecords = (PageSize * pageNumber) - PageSize;

                var modelList = _unitOfWork.GenericRepository<Contact>().GetAll(includeProperties:"Hospital")
                    .Skip(excludeRecords).Take(PageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Contact>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModel(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<ContactViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = PageSize
            };
            return result;

        }

        public ContactViewModel GetHospitalById(int ContactId)
        {
           var model = _unitOfWork.GenericRepository<Contact>().GetById(ContactId);
            var vm = new ContactViewModel(model);
            return vm;
        }

        public void InsertHospitalInfo(ContactViewModel contact)
        {
            var model = new ContactViewModel().ConvertViewModel(contact);
            _unitOfWork.GenericRepository<Contact>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateHospitalInfo(ContactViewModel contact)
        {
            var model = new ContactViewModel().ConvertViewModel(contact);
            var modelById = _unitOfWork.GenericRepository<Contact>().GetById(model.Id);
            modelById.Phone= contact.Phone;
            modelById.Email= contact.Email;
            modelById.HospitalId = contact.HospitalInfoId;

                _unitOfWork.GenericRepository<Contact>().Update(modelById);
                _unitOfWork.Save();
            
        }
        private List<ContactViewModel> ConvertModelToViewModel(List<Contact> modelList)
        {
            return modelList.Select(x=>new ContactViewModel(x)).ToList();
        }
    }
}
