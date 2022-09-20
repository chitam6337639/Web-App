using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using System.Threading.Tasks;
//show ds category
namespace WebApp.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent 
    {
        private readonly ApplicationDbContext _context;

        public CategoriesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> catList = await this._context.Categoris.ToListAsync();

            return View(catList);
        }
       
    }
}
