using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatMonOnline.Admin
{
    public partial class NguoiDung : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Người dùng";
                LayDuLieu();
            }
            //lbMessage.Visible = false;
        }

        protected void repeatNguoiDung_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
                cmd = new SqlCommand("NGUOIDUNG_CRUD", cn);
                cmd.Parameters.AddWithValue("@action", "DELETE");
                cmd.Parameters.AddWithValue("@userID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    lbMessage.Visible = true;
                    lbMessage.Text = "Xóa người dùng thành công!";
                    lbMessage.CssClass = "alert alert-success";
                    LayDuLieu();
                }
                catch (Exception ex)
                {
                    lbMessage.Visible = true;
                    lbMessage.Text = "Lỗi: " + ex.Message;
                    lbMessage.CssClass = "alert alert-danger";
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        protected void repeatNguoiDung_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        private void LayDuLieu()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("NGUOIDUNG_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "SELECTFORADMIN");
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            // thêm dữ liệu bằng repeater
            repeatNguoiDung.DataSource = dt;
            repeatNguoiDung.DataBind();
        }
    }
}