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

        }
        private void LayDuLieuDM()
        {
            //cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            //cmd = new SqlCommand("CATEGORY_CRUD", cn);
            //cmd.Parameters.AddWithValue("@action", "SELECT");
            //cmd.CommandType = CommandType.StoredProcedure;

            //da = new SqlDataAdapter(cmd);
            //dt = new DataTable();
            //da.Fill(dt);
            
            //repeatCategory.DataSource = dt;
            //repeatCategory.DataBind();           

        }
        private void LayDuLieuSP()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("SANPHAM_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            
            repeatSanPham.DataSource = dt;
            repeatSanPham.DataBind();
            //imgSanPham.ImageUrl = String.Empty;

        }

    }
}