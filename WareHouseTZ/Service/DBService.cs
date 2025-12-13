using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Date;
using WareHouseTZ.Modal;
using WareHouseTZ.Service;
using WareHouseTZ.Service.Response;

namespace WareHouseTZ.Service
{
    public class DBService : IDBService
    {
        private readonly DBApplication dbApplication;

        public DBService(DBApplication dbApplication)
        {
            this.dbApplication = dbApplication;
        }

        public async Task<DBResponse> AddProductDBAsync(Product product)
        {
            try
            {
                await dbApplication.Product.AddAsync(product);
                await dbApplication.SaveChangesAsync();
                return new DBResponseMessage("Продукт добавлен", true);
            }
            catch (Exception ex)
            {
                return new ErrorsResponse("Ошибка при добавлении продукта");
            }
        }
        //public async Task<DBResponse> GetProductDBAsync(int product_id)
        //{
        //    try
        //    {
        //        var product = await dbApplication.Products.FindAsync(product_id);
        //        if(product is not null) return new GetProductResponse("Продукт найден", true, product);
        //        else return new ErrorsResponse("Ошибка при получении продукта");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ErrorsResponse(ex.Message, "Ошибка при получении продукта");
        //    }
        //}
        public async Task<DBResponse> GetAllProductsDBAsync(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                var products = await dbApplication.Product.ToListAsync();
                token.ThrowIfCancellationRequested();
                ObservableCollection<Product> productsCollection = new ObservableCollection<Product>(products);
                if (products is not null) return new GetAllProductsResponse("Продукты найдены", true, productsCollection);
                else return new ErrorsResponse("Продуктов не найдено");
            }
            catch (OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                return new ErrorsResponse("Ошибка при получении продукта");
            }
        }

        public async Task<DBResponse> DeleteProductAsync(int product_id,CancellationToken token)
        {
            try
            {
                var product = await dbApplication.Product.FindAsync(product_id);
                token.ThrowIfCancellationRequested();
                if (product is null) return new ErrorsResponse("Продукт не найден");
                dbApplication.Product.Remove(product);
                token.ThrowIfCancellationRequested();
                await dbApplication.SaveChangesAsync();
                token.ThrowIfCancellationRequested();
                return new DBResponseMessage("Продукт удален", true);
            }
            catch(OperationCanceledException) { throw; }
            catch (Exception ex)
            {
                return new ErrorsResponse("Ошибка при удалении продукта");
            }
        }
        public async Task<DBResponse> UpdateProductAsync(Product product)
        {
            try
            {
                var existingProduct = await dbApplication.Product.FindAsync(product.Id);
                if (existingProduct is null) return new ErrorsResponse("Продукт не найден");
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Unit = product.Unit;
                dbApplication.Product.Update(existingProduct);
                await dbApplication.SaveChangesAsync();
                return new DBResponseMessage("Продукт обновлен", true);
            }
            catch (Exception ex)
            {
                return new ErrorsResponse("Ошибка при обновлении продукта");
            }
        }


        public async Task<DBResponse> CheckNameProductAsync(string Name)
        {
            try
            {
                var existingProduct = await dbApplication.Product.FirstOrDefaultAsync(c => c.Name == Name);
                if (existingProduct is null) return new DBResponseMessage("Имя свободно", true);
                else return new ErrorsResponse("Имя продукта уже есть в базе");
            }
            catch (Exception ex)
            {
                return new ErrorsResponse("Ошибка при поиске продукта");
            }
        }
        public async Task<DBResponse> EditCheckNameProductAsync(string Name,string OldName)
        {
            try
            {
                var existingProduct = await dbApplication.Product.FirstOrDefaultAsync(c => c.Name == Name);
                if (existingProduct is null) return new DBResponseMessage("Имя свободно", true);
                else if (existingProduct.Name == OldName) return new DBResponseMessage("Имена совпадают", true);
                else return new ErrorsResponse("Имя продукта уже есть в базе");
            }
            catch (Exception ex)
            {
                return new ErrorsResponse("Ошибка при поиске продукта");
            }
        }


        //Coming

        public async Task<DBResponse> AddComingDBAsync(Coming coming)
        {
            try
            {
                await dbApplication.Coming.AddAsync(coming);
                await dbApplication.SaveChangesAsync();
                return new DBResponseMessage("Продукт добавлен", true);
            }
            catch (Exception ex)
            {
                return new ErrorsResponse("Ошибка при добавлении прихода");
            }
        }

        public async Task<DBResponse> GetAllComingDBAsync()
        {
            try
            {
                var comings = await dbApplication.Coming.ToListAsync();
                ObservableCollection<Coming> comingsCollection = new ObservableCollection<Coming>(comings);
                if (comings is not null) return new GetAllComingResponse("Приходы найдены", true, comingsCollection);
                else return new ErrorsResponse("Продуктов не найдено");
            }
            catch (Exception ex)
            {
                return new ErrorsResponse("Ошибка при получении продукта");
            }
        }
        public async Task<DBResponse> DeleteComingAsync(int coming_id)
        {
            try
            {
                var coming = await dbApplication.Coming.FindAsync(coming_id);
                if (coming is null) return new ErrorsResponse("Приход не найден");
                dbApplication.Coming.Remove(coming);
                await dbApplication.SaveChangesAsync();
                return new DBResponseMessage("Coming удален", true);
            }
            catch (Exception ex)
            {
                return new ErrorsResponse("Ошибка при удалении продукта");
            }
        }
    }
}
