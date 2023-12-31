using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Interfaces
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Hotel GetHotelById(int id);
        List<Hotel> GetAllHotels();
        void Insert(Hotel hotel);
        void Delete(Hotel hotel);

    }
}
