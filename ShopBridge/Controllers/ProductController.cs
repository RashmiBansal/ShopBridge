using ShopBridge.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShopBridge.Models;
using ShopBridge.BusinessAccess;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    public class ProductController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        #region Add Product Item        
        [Route("product/addProductItem")]
        [HttpPost]
        public async Task<IHttpActionResult> AddProductItem(Product productInput)
        {
            try
            {
                //await new ProductBL().AddItem(productInput);
                return Content(HttpStatusCode.OK, await new ProductBL().AddItem(productInput)); 
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotImplemented, ex.Message); 
            }
        }
        #endregion

        #region Get List
        [Route("product/getProductList")]
        [HttpGet]
        public async Task<IHttpActionResult> GetProductList()
        {
            try
            {
                return Ok(await new ProductBL().GetProductList());                
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotImplemented, ex.Message);                             
            }            
        }
        #endregion

        #region Search Product Item by Name
        [Route("product/searchProduct")]
        [HttpGet]
        public async Task<IHttpActionResult> SearchProduct(string Name)
        {
            try
            {
                return Ok(await new ProductBL().SearchProduct(Name));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotImplemented, ex.Message);
            }
        }
        #endregion

        #region  Update Item
        [Route("product/updateProductItem")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateProductItem(int productId, Product productInput)
        {
            try
            {
                await new ProductBL().UpdateProductItem(productId, productInput);                
                return Content(HttpStatusCode.OK, "Successfully Updated");
            }
            catch (Exception ex)
            {                
                return Content(HttpStatusCode.NotImplemented, ex.Message);
            }
        }
        #endregion

        #region Delete product
        [Route("product/deleteProduct")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProductItem(int productId)
        {
            try
            {
                await new ProductBL().DeleteProductItem(productId);
                return Content(HttpStatusCode.OK, "Successfully Deleted");
            }
            catch (Exception ex)
            {   
                return Content(HttpStatusCode.NotImplemented, ex.Message);
            }
        }
        #endregion
        
    }
}
