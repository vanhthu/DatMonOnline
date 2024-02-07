using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace DatMonOnline.Admin
{
    public partial class SanPham : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Sản phẩm";
                LayDuLieu();
            }
            lbMessage.Visible = false;
        }

        protected void btnAdd0rUpdate_Click(object sender, EventArgs e)
        {
            // tạo biến
            string actionName = string.Empty;
            string imagePath = string.Empty;
            string fileExtention = string.Empty;

            bool isValidToExcute = false;
            int productID = Convert.ToInt32(hdnId.Value); // Hidden file


            // kết nối CSDL
            // lấy từ class
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());

            // tạo store procedure
            cmd = new SqlCommand("SANPHAM_CRUD", cn);

            cmd.Parameters.AddWithValue("@action", productID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@mota", txtThongTinSP.Text.Trim());
            cmd.Parameters.AddWithValue("@gia", txtGia.Text.Trim());
            cmd.Parameters.AddWithValue("@soluong", txtSoLuong.Text.Trim());
            cmd.Parameters.AddWithValue("@categoryID", ddlDanhMuc.SelectedValue);
            cmd.Parameters.AddWithValue("@isActive", cbIsActive.Checked);

            // xử lý hình ảnh 
            if (fuSanPhamImage.HasFile)
            {
                // trong phương thức KetNoi
                if (Utils.IsValidExtention(fuSanPhamImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtention = Path.GetExtension(fuSanPhamImage.FileName);
                    imagePath = "Images/SanPham/" + obj.ToString() + fileExtention;
                    fuSanPhamImage.PostedFile.SaveAs(Server.MapPath("~/Images/SanPham/") + obj.ToString() + fileExtention);
                    cmd.Parameters.AddWithValue("@imageURL", imagePath);
                    isValidToExcute = true;
                }
                else
                {
                    lbMessage.Visible = true;
                    lbMessage.Text = "Vui lòng chọn hình .jpg, .jpeg hoặc .png";
                    lbMessage.CssClass = "alert alert-danger";
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
                    actionName = productID == 0 ? "được thêm" : "được cập nhật";
                    lbMessage.Visible = true;
                    lbMessage.Text = "Sản phẩm " + actionName + " thành công!";
                    lbMessage.CssClass = "alert alert-success";
                    LayDuLieu();
                    // phương thức xóa trắng các giá trị
                    clear();
                }
                catch (Exception ex)
                {
                    lbMessage.Visible = true;
                    lbMessage.Text = "Không thành công " + ex.Message;
                    lbMessage.CssClass = "alert alert-danger";
                }
                finally
                {
                    cn.Close();
                }
            }
        }
        private void LayDuLieu()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("SANPHAM_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            // thêm dữ liệu bằng repeater
            repeatSanPham.DataSource = dt;
            repeatSanPham.DataBind();
            imgSanPham.ImageUrl = String.Empty;

        }

        private void clear()
        {
            txtName.Text = String.Empty;
            txtThongTinSP.Text = String.Empty;
            txtGia.Text = String.Empty;
            txtSoLuong.Text = String.Empty;
            ddlDanhMuc.ClearSelection();
            cbIsActive.Checked = false;
            hdnId.Value = "0";
            btnAdd0rUpdate.Text = "Add";
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void repeatSanPham_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void repeatSanPham_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}