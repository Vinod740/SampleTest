using System.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingCart.DAL;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class AdminController : Controller
    {
        ItemDAL itemDal;
        public AdminController(IConfiguration configuration)
        {
            itemDal = new ItemDAL(configuration);
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {            
            List<ItemMaster> items = itemDal.GetAllItems();
            return View(items);
        }

        public ActionResult Items()
        {
            List<ItemMaster> items = itemDal.GetAllItems();
            return View(items);
        }
        

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }

        public ActionResult UserList()
        {
            List<UserDetail> items = itemDal.GetAllUsers();
            return View(items);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Users(UserDetail userDetail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                string photoFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profile/" + userDetail.Photo.FileName);
                string photoPath = "/profile/" + userDetail.Photo.FileName;

                using (var stream = new FileStream(photoFullPath, FileMode.Create)) 
                {
                    userDetail.Photo.CopyTo(stream);
                    userDetail.PhotoPath = photoPath;
                }

                itemDal.InsertUser(userDetail);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemMaster itemMaster)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", itemMaster?.Image?.FileName);
                string imgPath = "\\img\\" + itemMaster?.Image?.FileName;
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    itemMaster.Image.CopyTo(stream);
                    itemMaster.ImagePath = imgPath;
                }

                itemDal.InsertItem(itemMaster);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
    }
}
