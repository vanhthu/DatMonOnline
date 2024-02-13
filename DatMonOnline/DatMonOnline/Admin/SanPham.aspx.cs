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
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/DangNhap.aspx");
                }
                else
                {
                    LayDuLieu();
                }
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
            lbMessage.Visible = false;
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            if (e.CommandName == "edit")
            {
                cmd = new SqlCommand("SANPHAM_CRUD", cn);
                cmd.Parameters.AddWithValue("@action", "GETBYID");
                cmd.Parameters.AddWithValue("@productID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                txtName.Text = dt.Rows[0]["name"].ToString();
                txtThongTinSP.Text = dt.Rows[0]["mota"].ToString();
                txtGia.Text = dt.Rows[0]["gia"].ToString();
                txtSoLuong.Text = dt.Rows[0]["soluong"].ToString();
                ddlDanhMuc.SelectedValue = dt.Rows[0]["categoryID"].ToString();
                cbIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["isActive"].ToString());
                imgSanPham.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["imageURL"].ToString()) ? "../Images/No_image.png" : "../" + dt.Rows[0]["imageURL"].ToString();
                imgSanPham.Width = 200;
                imgSanPham.Height = 200;
                hdnId.Value = dt.Rows[0]["productID"].ToString();

                btnAdd0rUpdate.Text = "Update";
                LinkButton btn = e.Item.FindControl("LinkButtonEdit") as LinkButton;
                btn.CssClass = "badge badge-warning";

            }
            else if (e.CommandName == "delete")
            {
                cmd = new SqlCommand("SANPHAM_CRUD", cn);
                cmd.Parameters.AddWithValue("@action", "DELETE");
                cmd.Parameters.AddWithValue("@productID", e.CommandArgument);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    lbMessage.Visible = true;
                    lbMessage.Text = "Xóa món ăn thành công!";
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

        protected void repeatSanPham_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblIsActive = e.Item.FindControl("lblIsActive") as Label;
                Label lblSoLuong = e.Item.FindControl("lblSoLuong") as Label;
                if (lblIsActive.Text == "True")
                {
                    lblIsActive.Text = "Active";
                    lblIsActive.CssClass = "badge badge-success";
                }
                else
                {
                    lblIsActive.Text = "In-Active";
                    lblIsActive.CssClass = "badge badge-danger";
                }
                if (Convert.ToInt32(lblSoLuong.Text) <= 5)
                {
                    lblSoLuong.CssClass = "badge badge-danger";
                    lblSoLuong.ToolTip = "";
                }
            }
        }
            
    }
} 