//using Hospital.Models;
//using Hospital.Repositories.Interface;
//using Hospital.Utility;
//using Hospital.ViewModels;


//namespace Hospital.Services
//{
//    public class HospitalInfoServices : IHospitalinfo
//    {
//        private IUnitOfWork _unitOfWork;

//        public HospitalInfoServices(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public void DeleteHospitalInfo(int id)
//        {
//            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(id);
//            _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
//            _unitOfWork.Save();
//        }

//        public PagedResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize)
//        {
//            var vm = new HospitalInfoViewModel();
//            int totalCount;
//            List<HospitalInfoViewModel>vmList = new List<HospitalInfoViewModel>();
//            try
//            {
//                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

//                var modelList = _unitOfWork.GenericRepository<HospitalInfo>().GetAll()
//                    .Skip(ExcludeRecords).Take(pageSize).ToList();

//                totalCount = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().ToList().Count;

//                vmList = ConvertModelToViewModelList(modelList);
//            }
//            catch(Exception)
//            {
//                throw;
//            }
//            var result =  new PagedResult<HospitalInfoViewModel>()
//            {
//                Data = vmList,
//                TotalItems = totalCount,
//                PageNumber = pageNumber,
//                PageSize = pageSize

//            };
//            return result;
//        }

//        public HospitalInfoViewModel GetHospitalById(int HospitaId)
//        {
//            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(HospitaId);
//            var vm = new HospitalInfoViewModel(model);
//            return vm;
//        }

//        public void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
//        {
//           var moidel = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
//            _unitOfWork.GenericRepository<HospitalInfo>().Add(model);
//            _unitOfWork.Save();


//        }

//        public void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
//        {
//          var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
//            var ModelById = _unitOfWork.GenericRepository<HospitalInfo>().GetById(model.Id);
//            ModelById.Name = hospitalInfo.Name;
//            ModelById.City = hospitalInfo.City;
//            ModelById.PinCode = hospitalInfo.PinCode;
//            ModelById.Country = hospitalInfo.Country;
//            _unitOfWork.GenericRepository<HospitalInfo>().Update(ModelById);
//            _unitOfWork.Save();

//        }

//        private List<HospitalInfoViewModel> ConvertModelToViewModelList(List<HospitalInfo> modelList)
//        {
//            return modelList.Select(x => new HospitalInfoViewModel(x)).ToList();
//        }

//    }
//} 
using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Utility;
using Hospital.ViewModels;

namespace Hospital.Services
{
    public class HospitalInfoServices : IHospitalinfo
    {
        private readonly IUnitOfWork _unitOfWork;

        public HospitalInfoServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Delete Hospital Info By it's Id
        /// </summary>
        /// <param name="id"></param>


        public void DeleteHospitalInfo(int id)
        {
            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(id);
            if (model != null)
            {
                _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
                _unitOfWork.Save();
            }
        }

        public PagedResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
            int totalCount;
            List<HospitalInfoViewModel> vmList = new List<HospitalInfoViewModel>();

            try
            {
                int excludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<HospitalInfo>().GetAll()
                    .Skip(excludeRecords).Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().Count();

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            return new PagedResult<HospitalInfoViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public HospitalInfoViewModel GetHospitalById(int hospitalId)
        {
            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(hospitalId);
            return model != null ? new HospitalInfoViewModel(model) : null;
        }

        public void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            _unitOfWork.GenericRepository<HospitalInfo>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            var modelById = _unitOfWork.GenericRepository<HospitalInfo>().GetById(model.Id);

            if (modelById != null)
            {
                modelById.Name = hospitalInfo.Name;
                modelById.City = hospitalInfo.City;
                modelById.PinCode = hospitalInfo.PinCode;
                modelById.Country = hospitalInfo.Country;

                _unitOfWork.GenericRepository<HospitalInfo>().Update(modelById);
                _unitOfWork.Save();
            }
        }

        private List<HospitalInfoViewModel> ConvertModelToViewModelList(List<HospitalInfo> modelList)
        {
            return modelList.Select(x => new HospitalInfoViewModel(x)).ToList();
        }

        System.Linq.Dynamic.Core.PagedResult<HospitalInfoViewModel> IHospitalinfo.GetAll(int pageNumber, int PageSize)
        {
            throw new NotImplementedException();
        }
    }
}
