using System;
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
            cmd = new SqlCommand("Category_Crud", cn);

            cmd.Parameters.AddWithValue("@Action", categoryID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@CategoryID", categoryID);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@IsActive", cbIsActive.Checked);

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
                    cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    isValidToExcute = true;
                }
                else
                {
                    
                }
            }
            else
            {

            }

        }
    }
}