using Jewellery_Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Jewellery_DataAccess.Repositories.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update (Product obj);

        IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
