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
        Task<DBResponse> GetAllProductsDBAsync(CancellationToken _token);
        Task<DBResponse> DeleteProductAsync(int produc_id, CancellationToken token);
        Task<DBResponse> UpdateProductAsync(Product product);
        Task<DBResponse> CheckNameProductAsync(string Name);
        Task<DBResponse> EditCheckNameProductAsync(string Name,string OldName);

        //Coming

        Task<DBResponse> AddComingDBAsync(Coming coming);
        Task<DBResponse> GetAllComingDBAsync();
        Task<DBResponse> DeleteComingAsync(int coming_id);
    }
}
