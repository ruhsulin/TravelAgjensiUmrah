namespace TravelAgjensiUmrah.App.Interfaces
{
    public interface IUserService
    {
        string GetUserId();
        string GetUserName();
        string GetUserEmail();
        string GetUserPhoneNumber();
        string GetUserRole();
        string GetFullName();
        string GetProfilePicture(bool thumbnail);
    }
}
