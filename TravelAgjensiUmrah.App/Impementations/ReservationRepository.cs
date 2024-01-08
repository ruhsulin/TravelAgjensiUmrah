using ravelAgjensiUmrah.App.Impementations;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;


        public ReservationRepository(TravelAgencyUmrahContext travelAgencyUmrahContext) : base(travelAgencyUmrahContext)
        {
            _travelAgencyUmrahContext = travelAgencyUmrahContext;
        }

        public Reservation GetReservationById(int id)
        {
            return _travelAgencyUmrahContext.Reservations.Find(id);
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _travelAgencyUmrahContext.Reservations.ToList();
        }

        public void AddReservation(Reservation reservation)
        {
            _travelAgencyUmrahContext.Reservations.Add(reservation);
        }

        public void UpdateReservation(Reservation reservation)
        {
            _travelAgencyUmrahContext.Reservations.Update(reservation);
        }

        public void Delete(Reservation res)
        {
            _travelAgencyUmrahContext.Reservations.Remove(res);
        }

        public void Save()
        {
            _travelAgencyUmrahContext.SaveChanges();
        }
    }

}
