using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Reservation GetReservationById(int id);
        IEnumerable<Reservation> GetAllReservations();
        void AddReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void Delete(Reservation reservation);
        void Save();
    }

}
