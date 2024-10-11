using AutoMapper;
using Entity.Services;
using Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ComputerPartSales.ViewComponents
{
    public class CategoryViewComponent:ViewComponent
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;


        public CategoryViewComponent(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //Viewbag.SelectedCategoryId = RouteData?.Values["id"];
            var categories = await _categoryService.GetAll();
            return View(_mapper.Map<List<CategoryViewModel>>(categories));
        }
    }
}
