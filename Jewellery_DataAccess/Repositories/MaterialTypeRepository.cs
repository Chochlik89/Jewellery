using Jewellery_DataAccess.Repositories.IRepositories;
using Jewellery_Models;

namespace Jewellery_DataAccess.Repositories
{
    public class MaterialTypeRepository : Repository<MaterialType>, IMaterialTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public MaterialTypeRepository (ApplicationDbContext db) : base(db)
        {
            _db = db;    
        }
        public void Update(MaterialType obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
            }
        }
    }
}
