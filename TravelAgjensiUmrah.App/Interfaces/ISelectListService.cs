using TravelAgjensiUmrah.Models.KeyValues;

namespace TravelAgjensiUmrah.App.Interfaces
{
    public interface ISelectListService
    {
        IEnumerable<KeyValueItem> GetRolesKeysValues();
    }
}
