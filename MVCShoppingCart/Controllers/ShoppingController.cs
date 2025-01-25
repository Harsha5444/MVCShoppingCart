using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping.BLL;

namespace MVCShoppingCart.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ShoppingBLL _bll;

        public ShoppingController()
        {
            _bll = new ShoppingBLL();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            var products = _bll.GetProducts();
            return View(products);
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (_bll.LoginVerify(username, password))
            {
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Invalid username or password.";            
            return View();
        }

        public ActionResult Register(string username, string fullname, string password, string mobilenumber)
        {
            if (_bll.RegisterUser(username, fullname, password, mobilenumber))
            {
                return RedirectToAction("Login");
            }
            ViewBag.Message = "Registration failed.";
            return View();
        }

        public ActionResult AddToCart(int productId, int quantity, string username)
        {
            if (_bll.AddToCart(productId, quantity, username))
            {
                return RedirectToAction("Cart");
            }
            ViewBag.Message = "Unable to add to cart.";
            return View("Error");
        }

        public ActionResult Cart()
        {
            var cartItems = _bll.GetCart();
            return View(cartItems);
        }

        public ActionResult Checkout(string username)
        {
            var result = _bll.Checkout(username);
            ViewBag.TotalCost = result.totalcost;
            ViewBag.OrderDate = result.OrderDate;
            ViewBag.OrderDetails = result.OrderDetails;
            return View("CheckoutSummary");
        }
    }
}