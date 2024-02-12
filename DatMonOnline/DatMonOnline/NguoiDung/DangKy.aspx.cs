using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.AccessControl;
using Microsoft.SqlServer.Server;
using System.IO;
using System.Net.Cache;

namespace DatMonOnline.NguoiDung
{
    public partial class DangKy : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    LayThongTinNguoiDung();
                }
                else if (Session["userID"] != null)
                {
                    Response.Redirect("TrangChu.aspx");
                }
            }
        }

        protected void btnDangKy_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty;
            string imagePath = string.Empty;
            string fileExtension = string.Empty;

            bool isValidToExcute = false;

            int userID = Convert.ToInt32(Request.QueryString["id"]);

            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("NGUOIDUNG_CRUD", cn);

            cmd.Parameters.AddWithValue("@action", userID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@userName", txtUserName.Text.Trim());
            cmd.Parameters.AddWithValue("@sdt", txtSoDT.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@diachi", txtDiaChi.Text.Trim());
            cmd.Parameters.AddWithValue("@postCode", txtMaXacNhan.Text.Trim());
            cmd.Parameters.AddWithValue("@matKhau", txtMatKhau.Text.Trim());

            if (fuUserImage.HasFile)
            {
                if (Utils.IsValidExtention(fuUserImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuUserImage.FileName);
                    imagePath = "Images/NguoiDung" + obj.ToString() + fileExtension;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/NguoiDung/")+ obj.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@imageURL", imagePath);
                    isValidToExcute = true;
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Vui lòng chọn hình .jpg, .jpeg hoặc .png";
                    lblMessage.CssClass = "alert alert-danger";
                    isValidToExcute = false;
                }
            }
            else
            {
                isValidToExcute = true;
            }

            if (isValidToExcute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    actionName = userID == 0 ? " Đăng ký thành công! <b><a href='DangNhap.aspx'>Nhấn vào đây</a></b> để đăng nhập" :
                        "Thông tin được cập nhật thành công! <b><a href='ThongTin.aspx'>Nhấn vào đây</a></b> ";
                    lblMessage.Visible = true;
                    lblMessage.Text = "<b>" + txtUserName.Text.Trim() +"<b>" + actionName;
                    lblMessage.CssClass = "alert alert-success";
                    if(userID != 0)
                    {
                        Response.AddHeader("REFRESH", "1;URL=ThongTin.aspx");
                    }
                    clear();

                }
                catch(SqlException ex)
                {
                    if(ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "<b>" + txtUserName.Text.Trim() + "<b>" + "tên đã tồn tại. Vui lòng nhập tên khác!";
                        lblMessage.CssClass = "alert alert-danger";
                    }                    
                }
                catch(Exception ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Không thành công " + ex.Message;
                    lblMessage.CssClass = "alert alert-danger";
                }
                finally
                {
                    cn.Close();
                }

            }

        }

        // int userID = Convert.ToInt32(Request.QueryString["id"]); có thể truy cập thông tin người dùng thông qua ID
        private void LayThongTinNguoiDung()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("NGUOIDUNG_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "SELECTFORINFO");
            cmd.Parameters.AddWithValue("@userID", Request.QueryString["id"]);
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows.Count == 1)
            {
                txtName.Text = dt.Rows[0]["name"].ToString();
                txtUserName.Text = dt.Rows[0]["userName"].ToString();
                txtSoDT.Text = dt.Rows[0]["sdt"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString();
                txtDiaChi.Text = dt.Rows[0]["diachi"].ToString();
                txtMaXacNhan.Text = dt.Rows[0]["postCode"].ToString();
                imgUser.ImageUrl = String.IsNullOrEmpty(dt.Rows[0]["imageURL"].ToString()) ?
                    "../Images/No_image.png" :
                    "../" + dt.Rows[0]["imageURL"].ToString();

                imgUser.Width = 200;
                imgUser.Height = 200;
                txtMatKhau.TextMode = TextBoxMode.SingleLine;
                txtMatKhau.ReadOnly = true;
                txtMatKhau.Text = dt.Rows[0]["matKhau"].ToString();

            }
            lblHeaderMessage.Text = "<h2>Cập nhật thông tin</h2>";
            btnDangKy.Text = "Cập nhật";
            lblDaDK.Text = "";
        }
        private void clear()
        {
            txtName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtSoDT.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtMaXacNhan.Text = string.Empty;
            txtMatKhau.Text = string.Empty;

        }
    }

}