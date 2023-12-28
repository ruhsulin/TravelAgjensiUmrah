using TravelAgjensiUmrah.Data.Entities;

namespace TravelAgjensiUmrah.App.Interfaces
{
    public interface IUserRepository : IRepository<AspNetUser>
    {
        AspNetUser? GetByStringId(string? id);
        //  List<AspNetUser> GetAllWithRoles();
        //  UserPicture? GetUserPicture(string id);
        // string GetProfilePicturePath(string userId, int thumbnail);
        //  void DeleteUserPicture(UserPicture userPicture);
        // void AddUserPicture(UserPicture userPicture);
    }
}
