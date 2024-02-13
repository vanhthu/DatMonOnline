using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatMonOnline.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbDangXuat_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("../NguoiDung/DangNhap.aspx");
        }
    }
}