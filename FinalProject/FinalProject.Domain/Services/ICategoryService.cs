using FinalProject.Domain.Models.Categories;

namespace FinalProject.Domain.Services
{
    public interface ICategoryService
	{
		List<CategoryViewModel> GetCategories();
	}
}
