using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Utility;
using Hospital.ViewModels;
using Microsoft.Extensions.Logging;

namespace Hospital.Services
{
    public class HospitalInfoServices : IHospitalInfo
    {
        private readonly ILogger<HospitalInfoServices> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HospitalInfoServices(IUnitOfWork unitOfWork, ILogger<HospitalInfoServices> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void DeleteHospitalInfo(int id)
        {
            var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(id);
            if (model != null)
            {
                _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
                _unitOfWork.Save();
                _logger.LogInformation("HospitalInfoServices-DeleteHospitalInfo:Deleted Sucessfully");

            }

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

        PagedResult<HospitalInfoViewModel> IHospitalInfo.GetAll(int pageNumber, int PageSize)
        {
            var vm = new HospitalInfoViewModel();
            int totalCount;
            List<HospitalInfoViewModel> vmList = new List<HospitalInfoViewModel>();

            try
            {
                int excludeRecords = (PageSize * pageNumber) - PageSize;

                var modelList = _unitOfWork.GenericRepository<HospitalInfo>().GetAll()
                    .Skip(excludeRecords).Take(PageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().ToList().Count;

                vmList = ConvertModelToViewModelList(modelList);
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PagedResult<HospitalInfoViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = PageSize
            };
           return result;

        }
    }
}
