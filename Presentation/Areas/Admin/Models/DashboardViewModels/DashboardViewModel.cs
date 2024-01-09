
namespace Presentation.Areas.Admin.Models.DashboardViewModels
{
    public class DashboardViewModel
    {
        public int NoPackages { get; set; }
        public int NoUsers { get; set; }
        public int NoReservations { get; set; }
        public int NoHotels { get; set; }

        public List<ReservationData> ReservationsByDate { get; set; }


        //public OrderChart ChartData { get; set; }
    }

    public class ReservationData
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
