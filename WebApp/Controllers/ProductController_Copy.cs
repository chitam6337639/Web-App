//using Microsoft.AspNetCore.Mvc;
//using System.Diagnostics;
//using WebApp.Models;
//using WebApp.Data;
//using Newtonsoft.Json;
//using Microsoft.AspNetCore.Http;
//using WebApp.Service;

//namespace WebApp.Controllers
//{
//    public class ProductController_Copy
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly CartService _cartService;

//        public ProductController(ApplicationDbContext context, CartService cartService)
//        {
//            _context = context;

//            _cartService = cartService;


//        }


//        public IActionResult Index()
//        {
//            //Hien Thi danh sach san pham
//            var products = _context.Products.ToList();
//            return View();
//        }
//        public IActionResult Detail(int id)
//        {
//            // Get all products
//            List<Product> productList = this._context.Products.ToList();

//            //  Find productId in productList.
//            Product productDetail = this._context.Products.Where(p => p.ProductId == id).FirstOrDefault() ?? new Product();
//            //FirstOrDefault(element => element.ProductId == id) ;


//            // Case productDetail is available in database.
//            return View(productDetail);
//        }


//        [Route("addcart/{productid:int}", Name = "addcart")]
//        public IActionResult AddToCart([FromRoute] int productid)
//        {

//            var product = _context.Products
//                .Where(p => p.ProductId == productid)
//                .FirstOrDefault();
//            if (product == null)
//                return NotFound("Không có sản phẩm");

//            // Xử lý đưa vào Cart ...
//            var cart = _cartService.GetCartItems();
//            var cartitem = cart.Find(p => p.product.ProductId == productid);
//            if (cartitem != null)
//            {
//                // Đã tồn tại, tăng thêm 1
//                cartitem.quantity++;
//            }
//            else
//            {
//                //  Thêm mới
//                cart.Add(new CartItem() { quantity = 1, product = product });
//            }

//            // Lưu cart vào Session
//            _cartService.SaveCartSession(cart);
//            // Chuyển đến trang hiện thị Cart
//            return RedirectToAction(nameof(Cart));
//        }
//        // Hiện thị giỏ hàng
//        [Route("/cart", Name = "cart")]
//        public IActionResult Cart()
//        {
//            return View(_cartService.GetCartItems());
//        }
//    }
//}
