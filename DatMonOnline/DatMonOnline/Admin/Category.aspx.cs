﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using System.IO;
using System.Drawing.Printing;

namespace DatMonOnline.Admin
{
    
    public partial class Category : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadCrum"] = "Category";
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
            int categoryID = Convert.ToInt32(hdnId.Value); // Hidden file


            // kết nối CSDL
            // lấy từ class
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());

            // tạo store procedure
            cmd = new SqlCommand("CATEGORY_CRUD", cn);

            cmd.Parameters.AddWithValue("@action", categoryID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@categoryID", categoryID);
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@isActive", cbIsActive.Checked);

            // xử lý hình ảnh 
            if (fuCategoryImage.HasFile)
            {
                // trong phương thức KetNoi
                if (Utils.IsValidExtention(fuCategoryImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtention = Path.GetExtension(fuCategoryImage.FileName);
                    imagePath = "Images/Category/" + obj.ToString() + fileExtention;
                    fuCategoryImage.PostedFile.SaveAs(Server.MapPath("~/Images/Category/") + obj.ToString() + fileExtention);
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
                isValidToExcute=true;
            }
            if (isValidToExcute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    actionName = categoryID == 0 ? "được thêm" : "được cập nhật";
                    lbMessage.Visible = true;
                    lbMessage.Text = "Danh mục " + actionName + " thành công!";
                    lbMessage.CssClass = "alert alert-success";
                    laydanhmuc();
                    // phương thức xóa trắng các giá trị
                    clear();
                }
                catch(Exception ex)
                {
                    lbMessage.Visible = true;
                    lbMessage.Text = "Không thành công "+ex.Message;
                    lbMessage.CssClass = "alert alert-danger";
                }
                finally
                {
                    cn.Close();
                }
            }
        }

        private void laydanhmuc()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("CATEGORY_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "SELECT");
            cmd.CommandType = CommandType.StoredProcedure;

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            // thêm dữ liệu bằng repeater
            repeatCategory.DataSource = dt;
            repeatCategory.DataBind();


        }

        private void clear()
        {
            txtName.Text = String.Empty;
            cbIsActive.Checked = false;
            hdnId.Value = "0";
            btnAdd0rUpdate.Text = "Add";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}