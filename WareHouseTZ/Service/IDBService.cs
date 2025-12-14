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
        Task<DBResponse> AddProductDBAsync(Product product, CancellationToken token);
        Task<DBResponse> GetAllProductsDBAsync(CancellationToken _token);
        Task<DBResponse> DeleteProductAsync(int produc_id, CancellationToken token);
        Task<DBResponse> UpdateProductAsync(Product product, CancellationToken token);
        Task<DBResponse> CheckNameProductAsync(string Name, CancellationToken token);
        Task<DBResponse> EditCheckNameProductAsync(string Name,string OldName, CancellationToken token);

        //Coming

        Task<DBResponse> AddComingDBAsync(Coming coming, CancellationToken token);
        Task<DBResponse> GetAllComingDBAsync(CancellationToken token);
        Task<DBResponse> DeleteComingAsync(int coming_id, CancellationToken token);
    }
}
