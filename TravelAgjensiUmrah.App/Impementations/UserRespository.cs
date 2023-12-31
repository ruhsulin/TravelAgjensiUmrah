using Microsoft.EntityFrameworkCore;
using ravelAgjensiUmrah.App.Impementations;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class UserRepository : Repository<AspNetUser>, IUserRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;
        public UserRepository(TravelAgencyUmrahContext travelAgencyUmrahContext) : base(travelAgencyUmrahContext)
        {
            _travelAgencyUmrahContext = travelAgencyUmrahContext;
        }

        public AspNetUser GetByStringId(string id)
        {
            return _travelAgencyUmrahContext.AspNetUsers.FirstOrDefault(x => x.Id == id)!;
        }

        public IEnumerable<AspNetUser> GetAll()
        {
            return _travelAgencyUmrahContext.AspNetUsers.ToList();
        }

        public List<AspNetUser> GetAllWithRoles()
        {
            return _travelAgencyUmrahContext.AspNetUsers.Include(x => x.Roles).ToList();
        }

        public UserPicture? GetUserPicture(string id)
        {
            return _travelAgencyUmrahContext.AspNetUsers.Include(x => x.Picture).FirstOrDefault(x => x.Id == id)?.Picture;
        }

        public void DeleteUserPicture(UserPicture userPicture)
        {
            _travelAgencyUmrahContext.UserPictures.Remove(userPicture);
            _travelAgencyUmrahContext.SaveChanges();
        }

        public void AddUserPicture(UserPicture userPicture)
        {
            _travelAgencyUmrahContext.UserPictures.Add(userPicture);
            _travelAgencyUmrahContext.SaveChanges();
        }

        public string GetProfilePicturePath(string userId, int thumbnail)
        {
            try
            {
                var upload = _travelAgencyUmrahContext.AspNetUsers.Include(x => x.Picture).FirstOrDefault(x => x.Id == userId)!.Picture;
                var path = "";
                if (upload != null)
                {
                    path = upload.Path;
                }

                var final = "";

                if (!string.IsNullOrEmpty(path))
                {
                    // remove ~
                    var pathwithoutsymbol = path.Substring(1, path.Length - 1);
                    //add_75
                    var splitted = pathwithoutsymbol.Split('.');
                    final = splitted[0] + "_" + thumbnail.ToString() + "." + splitted[1];
                }
                else
                {
                    final = "/uploads/notfound/notfound_75.png";
                }
                return final;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
