using ravelAgjensiUmrah.App.Impementations;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;
        public RoomTypeRepository(TravelAgencyUmrahContext travelAgencyUmrahContext) : base(travelAgencyUmrahContext)
        {
            _travelAgencyUmrahContext = travelAgencyUmrahContext;
        }

        public RoomType GetRoomTypeById(int id)
        {
            return _travelAgencyUmrahContext.RoomTypes.FirstOrDefault(x => x.Id == id);
        }

        public List<RoomType> GetAllRoomType()
        {
            return _travelAgencyUmrahContext.RoomTypes.ToList();
        }

        public void Insert(RoomType roomtype)
        {
            _travelAgencyUmrahContext.RoomTypes.Add(roomtype);
        }

        public void Update(RoomType roomtype)
        {
            _travelAgencyUmrahContext.RoomTypes.Update(roomtype);
        }

        public void Delete(RoomType roomtype)
        {
            _travelAgencyUmrahContext.RoomTypes.Remove(roomtype);
        }


    }

}
