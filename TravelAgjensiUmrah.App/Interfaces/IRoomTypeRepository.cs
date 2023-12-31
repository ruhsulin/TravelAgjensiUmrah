using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Interfaces
{
    public interface IRoomTypeRepository : IRepository<RoomType>
    {
        RoomType GetRoomTypeById(int id);
        List<RoomType> GetAllRoomType();
        void Insert(RoomType roomtype);
        void Delete(RoomType roomtype);
    }

}
