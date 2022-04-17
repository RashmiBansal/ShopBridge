using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ShopBridge.Context;
using ShopBridge.Models;

namespace ShopBridge.BusinessAccess
{
    public class ProductBL
    {
        DatabaseContext db = new DatabaseContext();

        #region Add product Item
        public async Task<int> AddItem(Product productInput)
        {
            if (string.IsNullOrEmpty(productInput.Name))
            {
                throw new Exception("Invalid Name {0}");
            }
            else
            {
                if (db.Products.Any(r => r.Name == productInput.Name.Trim()))
                {
                    throw new Exception("Name already exists.");
                }
                else
                {
                    Product newProduct = new Product();
                    Product productData = db.Products
                                            .Where(p => p.Name == productInput.Name.Trim())
                                            .FirstOrDefault();

                    if (productData == null)
                    {                        
                        newProduct = new Product()
                        {                            
                            Name = productInput.Name,
                            Description = productInput.Description,
                            Price = productInput.Price,
                            Quantity = productInput.Quantity,
                            CreatedAt = DateTime.Now
                        };
                        db.Products.Add(newProduct);
                    }
                    await db.SaveChangesAsync();
                    return newProduct.ProductId;
                }

            }
        }
        #endregion

        #region Get Product List
        public async Task<List<Product>> GetProductList()
        {
            var productList = from product in db.Products select product;
            return await productList.ToListAsync();
        }
        #endregion

        #region Search Product Item
        public async Task<List<Product>> SearchProduct(string Name)
        {
            var productList = from product in db.Products where product.Name.ToLower().Contains(Name.ToLower().Trim()) select product;
            return await productList.ToListAsync();            
        }
        #endregion

        #region Update Product Item
        public async Task UpdateProductItem(int productId, Product productInput)
        {
            Product product = db.Products.Where(p => p.ProductId == productId).FirstOrDefault();

            if (product != null)
            {
                product.Name = productInput.Name;
                product.Description = productInput.Description;
                product.Price = productInput.Price;
                product.Quantity = productInput.Quantity;
                product.ModifiedAt = DateTime.Now;
                await db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Product does not exist.");
            }
        }
        #endregion

        #region Delete Product Item
        public async Task DeleteProductItem(int productId)
        {
            if (productId != 0)
            {
                Product product = db.Products.Where(p => p.ProductId == productId).FirstOrDefault();
                
                if (product != null)
                {
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Product does not exist.");
                }
            }
            else
            {
                throw new Exception("Invalid Product.");
            }

        }
        #endregion
    }
}