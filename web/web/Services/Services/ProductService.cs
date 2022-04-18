using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using web.Utility;
using web.Web.Entity.Infrastructure;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;
using Web.Services.Mapping;

namespace web.Web.Services.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProduct();
        Task<IEnumerable<ProductDto>> GetAllActiveParentProduct();
        Task<IEnumerable<ProductDto>> GetAllActiveChildProduct();
        Task<ProductDto> GetProductById(int id);
        Response Insert(ProductDto dto);
        Task<Response> Update(ProductDto dto);
        Task<ProductDto> DropDownList(ProductDto dto);
        Task<IEnumerable<ProductPriceDto>> GetProductPriceByProductId(int productId);
        Task<ProductPriceDto> GetProductPriceById(int id);
        //Task<Response> InsertProductPrice(ProductPriceDto dto);
        Task<IEnumerable<ProductPriceDto>> GetActiveProductPriceByProductId(int ProductId);
        Task<Response> Delete(int id);
        //string DeletePrice(int id);
        //Task<string> UpdatePrice(int productPriceId);
        //Task<ProductPriceDto> ChangePrimaryProductPrice(int id);
        //string UpdatePrice(ProductPriceDto dto);
        Task<IEnumerable<ProductImageDto>> GetProductImageByProductId(int productId);
        //Task<string> InsertImage(ProductImageDto dto);
        //Task<string> DeleteImage(int id);
        //Task<string> UpdateImage(int id);
        Task<IEnumerable<ProductDto>> GetParentProductsWithChildProduct();
        Task<IEnumerable<ProductDto>> GetChildProductByParentProductId(int parentProductId);
        Task<IEnumerable<ProductDto>> GetDisplayProducts();
        Task<IPagedList<ProductDto>> GetDisplayProductsForProductPage(int pageNumber, int pageSize, string query, int? parentProductId);
    }

    public class ProductService : IProductService
    {
        private readonly Repository<Product> _repository;
        private MessageClass _messageClass;
        private SqlConnectionDetails _sql;
        //private IImageService _imageService;
        public ProductService()
           
        {
            _repository = new Repository<Product>();
            _messageClass = new MessageClass();
            //_imageService = imageService;
            _sql = _repository.GetSqlTransactionDetails();
        }

        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            return (await _repository.QueryAsync<ProductDto>("SELECT * FROM ProductView"));
        }

        public async Task<IEnumerable<ProductDto>> GetAllActiveParentProduct()
        {
            return (await _repository.QueryAsync<ProductDto>("SELECT * FROM ProductView " +
                "WHERE ParentProductId IS NULL AND Status=1"));
        }

        public async Task<IEnumerable<ProductDto>> GetAllActiveParentProductAsync()
        {
            return (await _repository.QueryAsync<ProductDto>("SELECT * FROM ProductView " +
                "WHERE ParentProductId IS NULL AND Status=1"));
        }

        public async Task<IEnumerable<ProductDto>> GetParentProductsWithChildProduct()
        {
            var obj = new List<ProductDto>();
            var list = (await GetAllActiveParentProductAsync());
            foreach (var l in list)
            {
                l.ChildProducts = await GetChildProductByParentProductId(l.ProductId);
                if (l.ChildProducts.Count() > 0)
                    obj.Add(l);
            }
            if (Convert.ToInt32(HttpContext.Current.Session["LangId"]) == 2)
                obj.ForEach(a => a.ProductName = a.ProductNameNepali);

            return obj;
        }

        public async Task<IEnumerable<ProductDto>> GetAllActiveChildProduct()
        {
            return (await _repository.QueryAsync<ProductDto>("SELECT * FROM ProductView " +
                "WHERE ParentProductId IS NOT NULL AND Status=1"));
        }

        public async Task<IEnumerable<ProductDto>> GetChildProductByParentProductId(int parentProductId)
        {
            var data = await _repository.QueryAsync<ProductDto>("SELECT * FROM ProductView " +
                "WHERE ParentProductId=@id AND Status=1", new { id = parentProductId });

            //data.ToList().ForEach(async a => a.ProductImage = (await GetProductImageByProductId(a.ProductId))?.FirstOrDefault());
            foreach (var d in data)
            {
                var image = await GetProductImageByProductId(d.ProductId);
                if (image.Count() > 0)
                    d.ProductImage = image.OrderByDescending(a => a.IsPrimary).FirstOrDefault();
            }
            return data;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            return (await _repository.QueryAsync<ProductDto>("SELECT * FROM ProductView " +
                "WHERE ProductId=@id", new { id })).FirstOrDefault();
        }

        public Response Insert(ProductDto dto)
        {
            var result = new Response();
            try
            {
                int resp = _repository.Insert(dto.ToEntity());
                result = _messageClass.SaveMessage(resp);
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<Response> Update(ProductDto dto)
        {
            var result = new Response();
            try
            {
                var product = await GetProductById(dto.ProductId);
                if (product is null)
                    return null;

                dto.CreatedBy = product.CreatedBy;
                dto.CreatedDate = product.CreatedDate;
                int resp = _repository.Update(dto.ToEntity());
                result = _messageClass.SaveMessage(product.ProductId);
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
            }
            return result;
        }

        public async Task<ProductDto> DropDownList(ProductDto dto)
        {
            if (dto is null)
                dto = new ProductDto();
            dto.GetActiveParentProduct = await GetAllActiveParentProduct();
            return dto;
        }

        public async Task<IEnumerable<ProductPriceDto>> GetProductPriceByProductId(int productId)
        {
            return (await _repository.QueryAsync<ProductPriceDto>("",productId));
        }

        public async Task<ProductPriceDto> GetProductPriceById(int id)
        {
            return (await _repository.QueryAsync<ProductPriceDto>("SELECT * FROM ProductPriceView " +
                "WHERE ProductId=@id", new { id })).FirstOrDefault();
        }

        public async Task<IEnumerable<ProductPriceDto>> GetActiveProductPriceByProductId(int ProductId)
        {
            return (await GetProductPriceByProductId(ProductId)).Where(a => a.Status == true);
        }

       
        public async Task<Response> Delete(int id)
        {
            var result = new Response();
            try
            {
                int resp =await _repository.DeleteAsync(id);
                result = _messageClass.DeleteMessage(resp);
            }
            catch (SqlException ex)
            {
                result.messageType = "error";
                result.message = ex.Message.ToString();
            }
            return result;
        }

        //public string DeletePrice(int id)
        //{
        //    string message = "";
        //    try
        //    {
        //        int result = _productRepository.DeletePrice(id);
        //        message = _messageClass.ShowDeleteMessage(result);
        //    }
        //    catch (SqlException ex)
        //    {
        //        message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
        //    }
        //    return message;
        //}

        //public async Task<string> UpdatePrice(int productPriceId)
        //{
        //    string message = "";
        //    var con = _baseInterface.GetConnection();
        //    var transaction = con.BeginTransaction();
        //    try
        //    {
        //        var productPrice = await _productRepository.GetProductPriceById(productPriceId);
        //        if (productPrice is null)
        //            return null;

        //        _productRepository.UpdatePriceByUnitIdProductId(productPrice.UnitId, productPrice.ProductId, con, transaction);
        //        _productRepository.UpdatePrice(productPriceId, con, transaction);

        //        message = _messageClass.ShowSuccessMessage(productPrice.ProductId);
        //        transaction.Commit();
        //    }
        //    catch (SqlException ex)
        //    {
        //        message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
        //        transaction.Rollback();
        //    }
        //    return message;
        //}

        //public async Task<ProductPriceDto> ChangePrimaryProductPrice(int id)
        //{
        //    try
        //    {
        //        var productPrice = await GetProductPriceById(id);
        //        if (productPrice is null)
        //            return null;

        //        bool? isPrimary = false;
        //        if (productPrice.IsPrimary == true)
        //            isPrimary = false;
        //        else isPrimary = true;

        //        productPrice.IsPrimary = isPrimary;
        //        UpdatePrice(productPrice);
        //        return productPrice;
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string UpdatePrice(ProductPriceDto dto)
        //{
        //    string message = "";
        //    try
        //    {
        //        int result = _productRepository.UpdatePrice(dto.ToEntity());
        //        message = _messageClass.ShowSuccessMessage(result);
        //    }
        //    catch (SqlException ex)
        //    {
        //        message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
        //    }
        //    return message;
        //}

        public async Task<IEnumerable<ProductImageDto>> GetProductImageByProductId(int productId)
        {
            var data = await _repository.QueryAsync<ProductImageDto>("SELECT * FROM ProductImage WHERE ProductId=@ProductId", new { ProductId=productId });
            data.ToList().ForEach(a => a.ImageString = Convert.ToBase64String(a.Photo));
            return data;
        }

        public async Task<IEnumerable<ProductDto>> GetDisplayProducts()
        {
            var obj = await _repository.QueryAsync<ProductDto>("SELECT * FROM DisplayProductView");
            return obj;
        }

        public async Task<IPagedList<ProductDto>> GetDisplayProductsForProductPage(int pageNumber, int pageSize, string query, int? parentProductId)
        {
            if (pageNumber <= 0)
                pageNumber = 1;

            var obj = await _repository.StoredProcedureAsync<ProductDto>("[dbo].[Sp_SearchProductForDisplay]", new { query = query, parentProductId = parentProductId });
            var data = obj.ToPagedList(pageNumber, pageSize);
            foreach (var x in data)
                x.GetProductPrice = await GetActiveProductPriceByProductId(x.ProductId);

            return data;
        }
    }
}