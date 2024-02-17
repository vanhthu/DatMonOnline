using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace DatMonOnline
{
    public class KetNoi
    {
        public static string LayChuoiKetNoi()
        {
            return ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

    }

    public class Utils
    {
        SqlConnection cn;        
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;

        public static bool IsValidExtention(string fileName)
        {
            bool isValid = false;
            string[] fileExtention = { ".jpg", ".png", ".jpeg"};

            for(int i=0; i <= fileExtention.Length - 1; i++)
            {
                if (fileName.Contains(fileExtention[i]))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        //
        public static string LayURLHinhAnh(object url)
        {
            string url1 = "";
            if(string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "../Images/No_image.png";
            }
            else
            {
                url1 = string.Format("../{0}", url);
            }
            return url1;

        }

        public bool CapNhatSoLuongTrongGioHang(int soluong, int productID, int userID)
        {
            bool isUpdated = false;
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("GIOHANG_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "UPDATE");
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@soluong", soluong);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                isUpdated = false;
                System.Web.HttpContext.Current.Response.Write("<script> alert('Lỗi - " + ex.Message + "'); </script>");
            }
            finally
            {
                cn.Close();
            }
            return isUpdated;

        }
        public int demSoLuongGioHang(int userID)
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("GIOHANG_CRUD" , cn);
            cmd.Parameters.AddWithValue("@action", "SELECT");
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();

            da.Fill(dt);
            return dt.Rows.Count;
        }
    }    

}