using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace DatMonOnline.NguoiDung
{
    public partial class ThongTinNguoiDung : System.Web.UI.Page
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
                    LayThongTinNguoiDung();
                }
            }
        }
        private void LayThongTinNguoiDung()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("NGUOIDUNG_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "SELECTFORINFO");
            cmd.Parameters.AddWithValue("@userID", Session["userID"]);
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 1)
            {
                Session["name"] = dt.Rows[0]["name"].ToString();
                Session["email"] = dt.Rows[0]["email"].ToString();
                Session["imageURL"] = dt.Rows[0]["imageURL"].ToString();
                Session["ngaytao"] = dt.Rows[0]["ngaytao"].ToString();
            }
        }
    }
}