using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatMonOnline.NguoiDung
{
    public partial class Menu : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LayDuLieuDM();
                LayDuLieuSP();
            }
        }
        private void LayDuLieuDM()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("CATEGORY_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "ACTIVE");
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            repeatCategory.DataSource = dt;
            repeatCategory.DataBind();

        }
        private void LayDuLieuSP()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("SANPHAM_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "ACTIVE");
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            
            repeatSanPham.DataSource = dt;
            repeatSanPham.DataBind();  
        }

        public string ChuyenDanhMucThanhChuThuong(object obj)
        {
            return obj.ToString().ToLower();
        }

        protected void repeatSanPham_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (Session["userID"] != null)
            {
                bool isCartItemUodated = false;
                int i = SanPhamTonTaiGioHang(Convert.ToInt32(e.CommandArgument));

                if (i == 0)
                {
                    // thêm sản phẩm vào đây
                    cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
                    cmd = new SqlCommand("GIOHANG_CRUD", cn);
                    cmd.Parameters.AddWithValue("@action", "INSERT");
                    cmd.Parameters.AddWithValue("@productID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@soluong", 1);
                    cmd.Parameters.AddWithValue("@userID", Session["userID"]);
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        Response.Write("<script>alert('Lỗi - " + ex.Message + "')</script>");
                    }
                    finally
                    {
                        cn.Close();
                    }

                }
                else
                {
                    //isCartItemUodated = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        int SanPhamTonTaiGioHang(int productID)
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("GIOHANG_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "GETBYID");            
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@userID", Session["userID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            int soLuong = 0;
            if(dt.Rows.Count > 0)
            {
                soLuong = Convert.ToInt32(dt.Rows[0]["soluong"]);
            }
            return soLuong;
        }
    }
}