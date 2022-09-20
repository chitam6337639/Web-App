using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly HttpContext _httpContext;
        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";
        //private readonly CartService _cartService;

        public ProductController(ApplicationDbContext context/*, CartService cartService, HttpContext httpContext */)
        {
            _context = context;
         //   this._httpContext = httpContext;

            //_cartService = cartService;


        }


        public IActionResult Index()
        {
            //Hien Thi danh sach san pham
            var products = _context.Products.ToList();
            return View();
        }
        public IActionResult Detail(int id)
        {
            // Get all products
            List<Product> productList = this._context.Products.ToList();

            //  Find productId in productList.
            Product productDetail = this._context.Products.Where(p => p.ProductId == id).FirstOrDefault() ?? new Product();
            //FirstOrDefault(element => element.ProductId == id) ;


            // Case productDetail is available in database.
            return View(productDetail);
        }


        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

       // Thêm sản phẩm vào cart
      //  [Route("addcart/{productid:int}", Name = "addcart")]
        [Route("addcart", Name = "AddToCart")]
        public IActionResult AddToCart([FromForm] int productid)
        {

            var product = _context.Products
                .Where(p => p.ProductId == productid)
                .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, product = product });
            }
           
            // Lưu cart vào Session
            SaveCartSession(cart);

            var access = new
            {
                quantity = cart.Count,
                
            };
            string jsoncart = JsonConvert.SerializeObject(access);
            
            // trả về json
            return Ok(jsoncart);
        }


        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }

        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }
        //check out
        public IActionResult CheckoutSS()
        {
            return View();
        }
        public IActionResult Checkout(IFormCollection Form)
        {
            try
            {
                Order _order = new Order();
                _order.AccountId = null;
                _order.CreatedDate = DateTime.Now;
                _order.QuanlityTotal = 0;
                _order.Total_Price = 0;
                _order.ShippingDate = DateTime.Now;
                _order.Status = "Dang Giao Hang";
                _order.Email_User = Form["EmAIL"];
                _order.Shipping_Address = Form["AddRess"];

                _context.Orders.Add(_order);
                _context.SaveChanges();
                return RedirectToAction("CheckoutSS");
            }
            catch
            {
                return Content("Error");
            }
        }
    }   
}
