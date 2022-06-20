using Jewellery_Models;

namespace Jewellery_DataAccess.Repositories.IRepositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update (Category obj);
    }
}
