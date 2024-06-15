using FinalProject.Domain.Models;
using FinalProject.Domain.Models.Products;

namespace FinalProject.Domain.Services
{
    public interface IProductService
    {
        Task<GenericData<ProductViewModel>> GetProducts(ProductPage model);
		Task<GenericData<ProductViewModel>> Get7Products();

		Task<ProductDetailViewModel> GetProductDetail(Guid productId);
        Task CreateProduct(ProductCreateViewModel model);
        Task DeleteProduct(Guid productId);
    }
}
