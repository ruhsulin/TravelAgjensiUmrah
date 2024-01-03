using ravelAgjensiUmrah.App.Impementations;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class HotelRepository : Repository<Hotel>, IHotelRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;
        public HotelRepository(TravelAgencyUmrahContext travelAgencyUmrahContext) : base(travelAgencyUmrahContext)
        {
            _travelAgencyUmrahContext = travelAgencyUmrahContext;
        }

        public Hotel GetHotelById(int id)
        {

            return _travelAgencyUmrahContext.Hotels.FirstOrDefault(x => x.Id == id);
        }

        public List<Hotel> GetAllHotels()
        {
            return _travelAgencyUmrahContext.Hotels.ToList();
        }

        public void Insert(Hotel hotel)
        {
            _travelAgencyUmrahContext.Hotels.Add(hotel);
        }

        public void Update(Hotel hotel)
        {
            _travelAgencyUmrahContext.Hotels.Update(hotel);
        }

        public void Delete(Hotel hotel)
        {
            _travelAgencyUmrahContext.Hotels.Remove(hotel);
        }

        public List<Hotel> GetHotelsByLocation(string location)
        {
            return _travelAgencyUmrahContext.Hotels
           .Where(h => h.Location == location)
           .ToList();
        }

    }
}
