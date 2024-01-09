using Microsoft.EntityFrameworkCore;
using ravelAgjensiUmrah.App.Impementations;
using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Data.Context;
using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class PackageRepository : Repository<Package>, IPackageRepository
    {
        protected readonly TravelAgencyUmrahContext _travelAgencyUmrahContext;
        public PackageRepository(TravelAgencyUmrahContext travelAgencyUmrahContext) : base(travelAgencyUmrahContext)
        {
            _travelAgencyUmrahContext = travelAgencyUmrahContext;
        }

        public Package GetPackageById(int id)
        {
            return _travelAgencyUmrahContext.Packages.FirstOrDefault(x => x.Id == id);
        }

        public List<Package> GetAllPackages()
        {
            return _travelAgencyUmrahContext.Packages.ToList();
        }

        public void Insert(Package package)
        {
            _travelAgencyUmrahContext.Packages.Add(package);
        }

        //public void Update(Package package)
        //{
        //   _travelAgencyUmrahContext.Packages.Update(package);
        // }

        public void Delete(Package package)
        {
            _travelAgencyUmrahContext.Packages.Remove(package);
        }


        public UserPicture? GetPackagePicture(int id)
        {
            return _travelAgencyUmrahContext.Packages.Include(x => x.Picture).FirstOrDefault(x => x.Id == id)?.Picture;
        }

        public void DeletePackagePicture(UserPicture packagePicture)
        {
            _travelAgencyUmrahContext.UserPictures.Remove(packagePicture);
            _travelAgencyUmrahContext.SaveChanges();
        }

        public void AddPackagePicture(UserPicture packagePicture)
        {
            _travelAgencyUmrahContext.UserPictures.Add(packagePicture);
            _travelAgencyUmrahContext.SaveChanges();
        }

        public string GetPackagePicturePath(int userId, int thumbnail)
        {
            try
            {
                var upload = _travelAgencyUmrahContext.Packages.Include(x => x.Picture).FirstOrDefault(x => x.Id == userId)!.Picture;
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

