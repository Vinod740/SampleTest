using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data;
using ShoppingCart.Models;
using System.Data;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        private static string _conn = string.Empty;
        public LoginController(IConfiguration configuration) 
        {
            _conn = configuration.GetConnectionString("DefaultConnection").ToString();
        }

        // GET: LoginController
        public ActionResult Index()
        {
            int UserGroup = Convert.ToInt16(HttpContext.Session.GetInt32("SessionUserId"));
            if (UserGroup != null)
            {
                if (UserGroup == 1)
                {
                    return RedirectToAction("Index", "Item");
                }
                else if (UserGroup == 2)
                {
                    return RedirectToAction("Index", "Item");
                }
            }
            return View();
        }

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserMaster userMaster)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                using (SqlConnection conn = new SqlConnection(_conn)) 
                {
                    SqlCommand cmd = new SqlCommand("SP_Login", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", userMaster.Email);
                    cmd.Parameters.AddWithValue("@Password", userMaster.Password);

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int UserGroup = dr.GetInt32("UserGroup");
                            int UserId = dr.GetInt32("UserId");
                            HttpContext.Session.SetInt32("SessionUserGroup", UserGroup);
                            HttpContext.Session.SetInt32("SessionUserId", UserId);                            
                            
                            if (UserGroup == 1)
                            {
                                return RedirectToAction("Index", "Item");                                
                            }
                            else if (UserGroup == 2)
                            {
                                return RedirectToAction("Index", "Item");
                            }
                        }
                    }
                    conn.Close();
                    ModelState.AddModelError("", "Wrong Email or Password");
                    return View();
                }                
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("SessionUserGroup");
            HttpContext.Session.Remove("SessionUserId");
            HttpContext.Session.Clear();            
            return View("Index");
        }


    }
}
