using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatMonOnline.Admin
{
    public partial class GiaoDienChinh : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["breadcrum"] = "";
                if (Session["admin"] == null)
                {
                    Response.Redirect("../User/DangNhap.aspx");
                }
            }
        }
    }
}