using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryDetail(int idm)
        {
            //Lấy ds product của category
            List<Product> productList = this._context.Products.Where(p => p.CategoryId == idm).ToList();
            return View(productList);


        }
    }
}
