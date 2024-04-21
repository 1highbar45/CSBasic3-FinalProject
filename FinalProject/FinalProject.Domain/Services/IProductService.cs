using FinalProject.Domain.Models;
using FinalProject.Domain.Models.Products;

namespace FinalProject.Domain.Services
{
    public interface IProductService
    {
        Task<GenericData<ProductViewModel>> GetProducts(ProductPage model);
        Task<ProductDetailViewModel> GetProductDetail(Guid productId);
    }
}
