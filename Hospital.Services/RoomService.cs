using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Utility;
using Hospital.ViewModels;

namespace Hospital.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteRoom(int id)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(id);
            if (model != null)
            {
                _unitOfWork.GenericRepository<Room>().Delete(model);
                _unitOfWork.Save();
            }
        }

        public PagedResult<RoomViewModel>GetAll(int pageNumber, int pageSize)
        {
            int totalCount;
            List<RoomViewModel> vmList;

            try
            {
                int excludeRecords = (pageSize * pageNumber) - pageSize;

                // Fetching rooms from the database with pagination
                var modelList = _unitOfWork.GenericRepository<Room>().GetAll(includeProperties:"Hospital")
                    .Skip(excludeRecords)
                    .Take(pageSize)
                    .ToList();

                totalCount = _unitOfWork.GenericRepository<Room>().GetAll().Count();

                // Converting Room entities to RoomViewModel list
                vmList = ConvertModelToViewModel(modelList);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching room data.", ex);
            }

            // Returning paginated result
            return new PagedResult<RoomViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public RoomViewModel GetRoomById(int roomId)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(roomId);
            return model != null ? new RoomViewModel(model) : null;
        }

        public void InsertRoom(RoomViewModel room)
        {
            var model = new Room
            {
                RoomNumber = room.RoomNumber,
                Type = room.Type,
                Status = room.Status,
                HospitalId = room.HospitalinfoId
            };

            _unitOfWork.GenericRepository<Room>().Add(model);
            _unitOfWork.Save();
        }

        public void UpdateRoom(RoomViewModel room)
        {
            var model = _unitOfWork.GenericRepository<Room>().GetById(room.Id);

            if (model != null)
            {
                model.RoomNumber = room.RoomNumber;
                model.Type = room.Type;
                model.Status = room.Status;
                model.HospitalId = room.HospitalinfoId;

                _unitOfWork.GenericRepository<Room>().Update(model);
                _unitOfWork.Save();
            }
        }

        private List<RoomViewModel> ConvertModelToViewModel(List<Room> modelList)
        {
            return modelList.Select(x => new RoomViewModel(x)).ToList();
        }
    }
}
