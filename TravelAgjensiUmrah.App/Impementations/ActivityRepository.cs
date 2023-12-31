using ravelAgjensiUmrah.App.Impementations;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class ActivityRepository : Repository<Activity>, IActivityRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;
        public ActivityRepository(TravelAgencyUmrahContext travelAgencyUmrahContext) : base(travelAgencyUmrahContext)
        {
            _travelAgencyUmrahContext = travelAgencyUmrahContext;
        }

        public Activity GetActivityById(int id)
        {
            return _travelAgencyUmrahContext.Activities.FirstOrDefault(x => x.Id == id);
        }

        public List<Activity> GetAllActivities()
        {
            return _travelAgencyUmrahContext.Activities.ToList();
        }

        public void Insert(Activity activity)
        {
            _travelAgencyUmrahContext.Activities.Add(activity);
        }
        public void Update(Activity activity)
        {
            _travelAgencyUmrahContext.Activities.Update(activity);
        }


        public void Delete(Activity activity)
        {
            _travelAgencyUmrahContext.Activities.Remove(activity);
        }


    }
}
