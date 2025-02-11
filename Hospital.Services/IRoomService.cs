using Hospital.Utility;
using Hospital.ViewModels;


namespace Hospital.Services
{
    public interface IRoomService
    {
        PagedResult<RoomViewModel>GetAll(int pageNumber, int pageSize);
        RoomViewModel GetRoomById(int RoomId);
        void UpdateRoom(RoomViewModel room);
        void InsertRoom(RoomViewModel room);
        void DeleteRoom(int id);

    }
}
