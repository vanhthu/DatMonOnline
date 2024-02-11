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
            cmd.Parameters.AddWithValue("@name", txtName.ToString().Trim());
            cmd.Parameters.AddWithValue("@userName", txtUserName.ToString().Trim());
            cmd.Parameters.AddWithValue("@sodt", txtSoDT.ToString().Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.ToString().Trim());
            cmd.Parameters.AddWithValue("@diaChi", txtDiaChi.ToString().Trim());
            cmd.Parameters.AddWithValue("@maXacNhan", txtMaXacNhan.ToString().Trim());
            cmd.Parameters.AddWithValue("@matKhau", txtMatKhau.ToString().Trim());

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
                    lblMessage.Text = "Vui lòng chọn .png .jpeg .jpg ";
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
                    actionName = userID == 0 ? "Đăng ký thành công! <b><a href='DangNhap.aspx'>Nhấn vào đây</a></b> để đăng nhập" :
                        "Thông tin được cập nhật thành công! <b><a href='ThongTin.aspx'>Nhấn vào đây</a></b> ";
                    lblMessage.Visible = true;
                    lblMessage.Text = "<b>" + txtUserName.ToString().Trim() +"<b>" + actionName;
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
                        lblMessage.Visible = false;
                        lblMessage.Text = "<b>" + txtUserName.ToString().Trim() + "<b>" + "tên đã tồn tại. Vui lòng nhập tên khác!";
                        lblMessage.CssClass = "alert alert-danger";
                    }                    
                }
                catch(Exception ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Lỗi: " +ex.Message;
                    lblMessage.CssClass = "alert alert-danger";
                }
                finally
                {
                    cn.Close();
                }

            }

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