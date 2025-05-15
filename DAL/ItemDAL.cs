using Microsoft.Data.SqlClient;
using System.Data;
using ShoppingCart.Models;
using System.Data.SqlTypes;
using System.Linq;

namespace ShoppingCart.DAL
{
    public class ItemDAL
    {
        private static string _conn = string.Empty;
        public ItemDAL(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DefaultConnection").ToString();
        }

        public List<ItemMaster> GetAllItems()
        {
            List<ItemMaster> listItem = new List<ItemMaster>();
            try
            {                
                using (SqlConnection conn = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ItemMaster", conn);
                    cmd.CommandType = CommandType.Text;
                    
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ItemMaster item = new ItemMaster() { ItemName = string.Empty };
                        item.ItemId = dr.GetInt32("ItemId");
                        item.ItemName = Convert.ToString(dr.GetString("ItemName"));
                        item.ImagePath = dr.GetString("Image");
                        item.Stock = dr.GetInt32("Stock");
                        item.Price = dr.GetDecimal("Price");
                        listItem.Add(item);
                    }
                    return listItem;
                }
            }
            catch (Exception ex)
            {
                Console.Write("" + ex.Message);
            }            

            return listItem;
        }

        public ItemMaster GetItemById(int itemId)
        {            
            try
            {
                List<ItemMaster> items = this.GetAllItems();
                ItemMaster item = items.Where(x => x.ItemId == itemId).First();
                return item;
            }
            catch (Exception ex)
            {
                Console.Write("" + ex.Message);
            }

            return null;
        }

        public bool InsertItem(ItemMaster itemMaster)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_Ins_Up_Item", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ItemId", itemMaster.ItemId);
                    cmd.Parameters.AddWithValue("@ItemName", itemMaster.ItemName);
                    cmd.Parameters.AddWithValue("@Image", itemMaster.ImagePath);
                    cmd.Parameters.AddWithValue("@Stock", itemMaster.Stock);
                    cmd.Parameters.AddWithValue("@Price", itemMaster.Price);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    //while (dr.Read())
                    //{
                    //    ItemMaster item = new ItemMaster();
                    //    item.ItemId = dr.GetInt32("ItemId");
                    //    item.ItemName = dr.GetString("ItemName");
                    //    item.Image = null; /// dr.GetString("Image");
                    //    item.Stock = dr.GetInt32("Stock");
                    //    item.Price = dr.GetDecimal("Price");

                    //    listItem.Add(item);
                    //}
                    //return listItem;
                }
            }
            catch (Exception ex)
            {
                Console.Write("" + ex.Message);
            }
            return true;

        }

        public List<UserDetail> GetAllUsers()
        {
            List<UserDetail> listItem = new List<UserDetail>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM UserDetails", conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        UserDetail item = new UserDetail();
                        item.UserDetailId = dr.GetInt32("UserDetailId");
                        item.Email = Convert.ToString(dr.GetString("Email"));
                        item.Nationality = dr.GetInt32("Nationality");
                        item.UserType = dr.GetInt32("UserType");
                        item.Gender = dr.GetBoolean("Gender");
                        item.DOB = dr.GetDateTime("DOB");
                        item.PhotoPath = dr.GetString("Photo");
                        listItem.Add(item);
                    }
                    return listItem;
                }
            }
            catch (Exception ex)
            {
                Console.Write("" + ex.Message);
            }

            return listItem;
        }

        public bool InsertUser(UserDetail userDetail)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_Ins_Up_User", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserDetailId", userDetail.UserDetailId);
                    cmd.Parameters.AddWithValue("@Email", userDetail.Email);
                    cmd.Parameters.AddWithValue("@Nationality", userDetail.Nationality);
                    cmd.Parameters.AddWithValue("@UserType", userDetail.UserType);
                    cmd.Parameters.AddWithValue("@DOB", userDetail.DOB);
                    cmd.Parameters.AddWithValue("@Gender", userDetail.Gender);
                    cmd.Parameters.AddWithValue("@PhotoPath", userDetail.PhotoPath);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    //while (dr.Read())
                    //{
                    //    ItemMaster item = new ItemMaster();
                    //    item.ItemId = dr.GetInt32("ItemId");
                    //    item.ItemName = dr.GetString("ItemName");
                    //    item.Image = null; /// dr.GetString("Image");
                    //    item.Stock = dr.GetInt32("Stock");
                    //    item.Price = dr.GetDecimal("Price");

                    //    listItem.Add(item);
                    //}
                    //return listItem;
                }
            }
            catch (Exception ex)
            {
                Console.Write("" + ex.Message);
            }
            return true;

        }

        public int AddtoCart(int ItemId, int UserId) 
        {
            int iReturn = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_conn))
                {                    
                    SqlCommand cmd1 = new SqlCommand("SP_CheckStockAvailable", conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@ItemId", ItemId);
                    cmd1.Parameters.AddWithValue("@UserId", UserId);
                    cmd1.Parameters.Add("@AvailableUnit", SqlDbType.Int).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    int output = Convert.ToInt32(cmd1.Parameters["@AvailableUnit"].Value);
                    if (output < 1) {
                        iReturn = 0;
                        conn.Close();
                        return iReturn;
                    }

                    string sqlChkStock = "INSERT INTO Cartdetail (ItemId, UserId, IsActive) VALUES ("+ ItemId+ ", "+ UserId+ ", "+ 1 +")";
                    SqlCommand cmd = new SqlCommand(sqlChkStock, conn);
                    cmd.CommandType = CommandType.Text;

                    //conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    iReturn = 1;
                }
            }
            catch (Exception ex)
            {
                Console.Write("" + ex.Message);
            }            
            return iReturn;
        }

        public List<ItemMaster> CheckOut(int userId)
        {
            List<ItemMaster> listItem = new List<ItemMaster>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_conn))
                {
                    string sqlString = "SELECT COUNT(cd.cartdetailId) As Stock , cd.ItemId, im.[Image], im.ItemName, im.Price, SUM(im.Price) AS Total FROM Cartdetail cd  INNER JOIN ItemMaster im on im.ItemId = cd.itemid WHERE cd.userid = " + userId + " AND ISactive = 1 GROUP BY cd.itemid,im.[Image], im.ItemName,im.Price";
                    SqlCommand cmd = new SqlCommand(sqlString, conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    decimal? grandTotal = 0;
                    while (dr.Read())
                    {                        
                        ItemMaster item = new ItemMaster() { ItemName = string.Empty };
                        item.ItemId = dr.GetInt32("ItemId");
                        item.ItemName = Convert.ToString(dr.GetString("ItemName"));
                        item.ImagePath = dr.GetString("Image");
                        item.Stock = dr.GetInt32("Stock");
                        item.Price = dr.GetDecimal("Price");
                        item.Total = dr.GetDecimal("Total");
                        grandTotal = grandTotal + item.Total;
                        item.GrandTotal = grandTotal;
                        listItem.Add(item);                        
                    }
                    return listItem;
                }
            }
            catch (Exception ex)
            {
                Console.Write("" + ex.Message);
            }

            return listItem;
        }

        public int DeleteFromCart(int ItemId, int UserId)
        {
            int iReturn = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_conn))
                {                    
                    string sqlChkStock = "WITH CTE AS (SELECT TOP 1 * FROM CARTDETAIL WHERE IsActive = 1 AND ItemId = "+ ItemId +" and UserId = " + UserId + ") UPDATE cte SET IsActive = 0";
                    SqlCommand cmd = new SqlCommand(sqlChkStock, conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    iReturn = 1;
                }
            }
            catch (Exception ex)
            {
                Console.Write("" + ex.Message);
            }
            return iReturn;
        }

        public int Payout(int UserId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_conn))
                {
                    string sqlItemCnt = "SELECT ItemId,COUNT(CartDetailId) InCart  FROM CartDetail WHERE IsActive = 1 AND UserId = "+ UserId  + " GROUP BY ItemId";
                    SqlCommand cmd1 = new SqlCommand(sqlItemCnt, conn);
                    cmd1.CommandType = CommandType.Text;                    

                    conn.Open();
                    SqlDataReader dr = cmd1.ExecuteReader();
                    List<ItemMaster> listItems = new List<ItemMaster>();
                    while (dr.Read())
                    {
                        ItemMaster item = new ItemMaster() { ItemName = string.Empty };
                        item.ItemId = dr.GetInt32("ItemId");
                        item.StockSold = dr.GetInt32("InCart");
                        listItems.Add(item);
                    }
                    conn.Close();

                    conn.Open();
                    foreach (var item in listItems)
                    {
                        string sqlUpdateStock = "UPDATE ItemMaster SET Stock = (Stock - " + item.StockSold + ") WHERE ItemId = " + item.ItemId;
                        SqlCommand cmd = new SqlCommand(sqlUpdateStock, conn);
                        cmd.CommandType = CommandType.Text;                        
                        cmd.ExecuteNonQuery();
                    }

                    string SqlEmptyCart = "UPDATE CartDetail SET IsActive = 0 WHERE UserId =" + UserId;
                    SqlCommand cmd3 = new SqlCommand(SqlEmptyCart, conn);
                    cmd3.CommandType = CommandType.Text;
                    cmd3.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.Write("" + ex.Message);
                return 0;
            }
            return 1;
        }
    }
}
