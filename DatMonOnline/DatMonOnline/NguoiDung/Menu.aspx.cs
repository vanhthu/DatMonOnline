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
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);


        }
    }
}