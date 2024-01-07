using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class ReservationRepository : IReservationRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;


        public ReservationRepository(TravelAgencyUmrahContext context)
        {
            _travelAgencyUmrahContext = context;
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

        public void DeleteReservation(int id)
        {
            var reservation = _travelAgencyUmrahContext.Reservations.Find(id);
            if (reservation != null)
            {
                _travelAgencyUmrahContext.Reservations.Remove(reservation);
            }
        }

        public void Save()
        {
            _travelAgencyUmrahContext.SaveChanges();
        }
    }

}
