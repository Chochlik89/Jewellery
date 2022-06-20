using Jewellery_Models;

namespace Jewellery_DataAccess.Repositories.IRepositories
{
    public interface IMaterialTypeRepository : IRepository<MaterialType>
    {
        void Update (MaterialType obj);
    }
}
