
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Interfaces
{
    public interface IActivityRepository : IRepository<Activity>
    {
        Activity GetActivityById(int id);
        List<Activity> GetAllActivities();
        void Insert(Activity activity);
        void Delete(Activity activity);
    }
}
