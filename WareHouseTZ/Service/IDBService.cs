using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Modal;

namespace WareHouseTZ.Service
{
    public interface IDBService
    {
        //Task<DBResponse> GetProductDBAsync(int product_id);
        Task<DBResponse> AddProductDBAsync(Product product);
        Task<DBResponse> GetAllProductsDBAsync();
        Task<DBResponse> DeleteProductAsync(int produc_id);
        Task<DBResponse> UpdateProductAsync(Product product);
        Task<DBResponse> CheckNameProductAsync(string Name);
        Task<DBResponse> EditCheckNameProductAsync(string Name,string OldName);
    }
}
