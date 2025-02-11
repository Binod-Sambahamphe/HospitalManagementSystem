//using Hospital.Models;
//using Hospital.Repositories.Interface;
//using Hospital.Utility;
//using Hospital.ViewModels;

//namespace Hospital.Services
//{
//    public class ApplicationUserService : IApplicationUserService
//    {
//        private IUnitOfWork _unitOfWork;

//        public ApplicationUserService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public PagedResult<ApplicationUserViewModel> GetAll(int PageNumber, int PageSize)
//        {
//            var vm = new ApplicationUserViewModel();
//            int totalCount;
//            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();
//            try
//            {
//                int ExcludeRecords = (PageSize * PageNumber) - PageSize;
//                var modelList = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().
//                    Skip(ExcludeRecords).Take(PageSize).ToList();
//                totalCount = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().ToList().Count;
//                vmList = ConvertModelToViewModel(modelList);
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//            var result = new PagedResult<ApplicationUserViewModel>
//                (
//                Data = vmList,
//                TotalItems = totalCount,
//                PageNumber = PageNumber,
//                PageSize = PageSize
//                );
//            return result;

//        }

//        public PagedResult<ApplicationUserViewModel> GetAllDoctor(int PageNumber, int PageSize)
//        {
//            throw new NotImplementedException();
//        }

//        public PagedResult<ApplicationUserViewModel> GetAllPatient(int PageNumber, int PageSize)
//        {
//            throw new NotImplementedException();
//        }

//        public PagedResult<ApplicationUserViewModel> SearchDoctor(int PageNumber, int PageSize, string Spicility = null)
//        {
//            throw new NotImplementedException();
//        }

//        private List<ApplicationUserViewModel> ConvertToViewModelList(List<ApplicationUserViewModel> modelList)
//        {
//            return modelList.Select(x => new ApplicationUserViewModel(x)).ToList();
//        }

//    }

//}


using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Utility;
using Hospital.ViewModels;

namespace Hospital.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PagedResult<ApplicationUserViewModel> GetAll(int PageNumber, int PageSize)
        {
            int totalCount = 0;
            List<ApplicationUserViewModel> vmList = new List<ApplicationUserViewModel>();

            try
            {
                int ExcludeRecords = (PageSize * PageNumber) - PageSize;
                var repository = _unitOfWork.GenericRepository<ApplicationUser>();

                var modelList = repository.GetAll()
                    .Skip(ExcludeRecords)
                    .Take(PageSize)
                    .ToList();

                totalCount = repository.GetAll().Count();
                vmList = ConvertToViewModelList(modelList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll: {ex.Message}");
                throw;
            }

            return new PagedResult<ApplicationUserViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = PageNumber,
                PageSize = PageSize
            };
        }

        public PagedResult<ApplicationUserViewModel> GetAllDoctor(int PageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public PagedResult<ApplicationUserViewModel> GetAllPatient(int PageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }

        public PagedResult<ApplicationUserViewModel> SearchDoctor(int PageNumber, int PageSize, string Spicility = null)
        {
            throw new NotImplementedException();
        }

        private List<ApplicationUserViewModel> ConvertToViewModelList(List<ApplicationUser> modelList)
        {
            return modelList.Select(x => new ApplicationUserViewModel(x)).ToList();
        }
    }
}
