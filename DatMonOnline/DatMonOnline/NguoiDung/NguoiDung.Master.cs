using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatMonOnline.NguoiDung
{
    public partial class NguoiDung : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.Url.AbsoluteUri.ToString().Contains("TrangChu.aspx"))
            {
                form1.Attributes.Add("class", "sub_page");
            }
            else
            {

                form1.Attributes.Remove("class");
                // load control
                Control sliderUserControl = (Control)Page.LoadControl("SliderUserControl.ascx");

                panelSlider.Controls.Add(sliderUserControl);
            }

            if (Session["userID"] != null)
            {
                lbDangNhapDangXuat.Text = "Đăng xuất";
            }
            else
            {
                lbDangNhapDangXuat.Text = "Đăng nhập";
            }

        }

        protected void lbDangNhapDangXuat_Click(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                Session.Abandon();
                Response.Redirect("DangNhap.aspx");
            }
        }

        protected void lbDangKyorThongTin_Click(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                lbDangKyorThongTin.ToolTip = "Thông tin người dùng";
                Response.Redirect("ThongTinNguoiDung.aspx");
            }
            else
            {
                lbDangKyorThongTin.ToolTip = "Đăng ký";
                Response.Redirect("DangKy.aspx");
            }
        }
    }
}