using TravelAgjensiUmrah.App.Interfaces;
using TravelAgjensiUmrah.Models.KeyValues;

namespace TravelAgjensiUmrah.App.Impementations
{
    public class SelectListService : ISelectListService
    {
        private readonly IRolesRepository rolesRepository;

        public SelectListService(IRolesRepository rolesRepository)
        {
            this.rolesRepository = rolesRepository;
        }

        public IEnumerable<KeyValueItem> GetRolesKeysValues()
        {
            try
            {
                var roles = rolesRepository.GetAll().ToList();
                var result = roles.Select(role => new KeyValueItem()
                {
                    SKey = role.Id,
                    Value = role.Name ?? "" //nese nuk eshte null merre vleren e null, nese jo mere vleren e emptyString ""
                });

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
