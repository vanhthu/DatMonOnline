using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatMonOnline.NguoiDung
{
    public partial class GioHang : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userID"] == null)
                {
                    Response.Redirect("DangNhap.aspx");
                }
                else
                {

                }
            }
            
        }

        protected void repeatSanPhamGioHang_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void repeatSanPhamGioHang_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        // lấy thông tin
        void LaySanPhamTuGioHang()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("GIOHANG_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "SELECT");            
            cmd.Parameters.AddWithValue("@userID", Session["userID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            repeatSanPhamGioHang.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                
            }
            repeatSanPhamGioHang.DataBind();
            
        }
    }
}