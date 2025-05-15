using System.Configuration;
using System.Data.SqlTypes;
using System.Net.Mime;
using System.Web;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Map;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using ShoppingCart.DAL;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class ItemController : Controller
    {
        ItemDAL itemDal;
        public ItemController(IConfiguration configuration) 
        {
            itemDal = new ItemDAL(configuration);
        }

        // GET: ItemController
        public ActionResult Index()
        {
            if (isLoggedIn())
            {
                ViewData["SessionUserGroup"] = HttpContext.Session.GetInt32("SessionUserGroup");
                ViewBag.SessionUserGroup = HttpContext.Session.GetInt32("SessionUserGroup");
                List<ItemMaster> items = itemDal.GetAllItems();
                return View(items);
            }
            return RedirectToAction("Index", "Login");
        }
        public ActionResult AddToCart(int id)
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
            if (itemDal.AddtoCart(id, userId) == 1)
            {
                TempData["AddedToCart"] = "<script>alert('Item Added to cart'); </script>";
            }            
            else
            {
                TempData["AddedToCart"] = "<script>alert('Stock not available'); </script>";
            }
            return RedirectToAction("Index");
        }

        public ActionResult CheckOut()
        {
            if (isLoggedIn())
            {
                try
                {
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
                    List<ItemMaster> items = itemDal.CheckOut(userId);
                    ViewData["GrandTotal"] = items.LastOrDefault()?.GrandTotal;
                    return View(items);
                } 
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Login"); 
        }

        public ActionResult Payout()
        {
            if (isLoggedIn())
            {
                try
                {
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));
                    var outPut = itemDal.Payout(userId);
                    if (outPut == 1)
                    {
                        TempData["Success"] = "<script>alert('Success !!')</script>";
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Login");
        }

        // GET: ItemController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemController/Edit/5
        public ActionResult Edit(int id)
        {            
            var itemData = itemDal.GetItemById(id);
            string imgPath = Directory.GetCurrentDirectory() + "/wwwroot/" + itemData?.ImagePath;
            string imgPath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/", itemData?.ImagePath);
            FileStream stream = new FileStream(imgPath, FileMode.Open, FileAccess.ReadWrite);
            var fsr = new FileStreamResult(stream, "image/jpg");
            var ms = new MemoryStream();
            fsr.FileStream.CopyTo(ms);
            itemData.Image = new FormFile(ms, 0, ms.Length, "name", fsr.FileDownloadName);

            //using (var fs = fsr.FileStream)
            //{
            //    itemData.Image = new FormFile(fs, 0, fs.Length, "name", fsr.FileDownloadName);
            //}

            return View(itemData);
        }

        // POST: ItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItemMaster item)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemController/Delete/5
        public ActionResult Delete(int id)
        {
            if (isLoggedIn())
            {
                try
                {
                    int userId = Convert.ToInt32(HttpContext.Session.GetInt32("SessionUserId"));                    
                    if (itemDal.DeleteFromCart(id, userId) == 1)
                    {
                        TempData["AddedToCart"] = "<script>alert('Removed from cart'); </script>";
                    }
                    return RedirectToAction("Checkout", "Item");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Login");
        }

        // POST: ItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private bool isLoggedIn()
        {
            if (HttpContext.Session.GetString("SessionUserId") != null)
            { 
                return true;
            }
            return false;
        }
    }
}
