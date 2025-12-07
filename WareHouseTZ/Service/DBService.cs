using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouseTZ.Date;
using WareHouseTZ.Modal;
using WareHouseTZ.Service;
using System.Collections.ObjectModel;

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
                return new ErrorsResponse(ex.Message, "Ошибка при добавлении продукта");
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
        public async Task<DBResponse> GetAllProductsDBAsync()
        {
            try
            {
                var products = await dbApplication.Product.ToListAsync();
                ObservableCollection<Product> productsCollection = new ObservableCollection<Product>(products);
                if (products is not null) return new GetAllProductsResponse("Продукты найдены", true, productsCollection);
                else return new ErrorsResponse("Продуктов не найдено");
            }
            catch (Exception ex)
            {
                return new ErrorsResponse(ex.Message, "Ошибка при получении продукта");
            }
        }

        public async Task<DBResponse> DeleteProductAsync(int product_id)
        {
            try
            {
                var product = await dbApplication.Product.FindAsync(product_id);
                if (product is null) return new ErrorsResponse("Продукт не найден");
                dbApplication.Product.Remove(product);
                await dbApplication.SaveChangesAsync();
                return new DBResponseMessage("Продукт удален", true);
            }
            catch (Exception ex)
            {
                return new ErrorsResponse(ex.Message, "Ошибка при удалении продукта");
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
                return new ErrorsResponse(ex.Message, "Ошибка при обновлении продукта");
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
                return new ErrorsResponse(ex.Message, "Ошибка при поиске продукта");
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
                return new ErrorsResponse(ex.Message, "Ошибка при поиске продукта");
            }
        }
    }
}
