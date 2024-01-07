using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Interfaces
{
    public interface IReservationRepository
    {
        Reservation GetReservationById(int id);
        IEnumerable<Reservation> GetAllReservations();
        void AddReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void DeleteReservation(int id);
        void Save();
    }

}
