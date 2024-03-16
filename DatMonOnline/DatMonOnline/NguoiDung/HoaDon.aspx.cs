using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatMonOnline.NguoiDung
{
    public partial class HoaDon : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        DataTable LayThongTinDatMon()
        {
            double grandTotal = 0;
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("HOADON", cn);
            cmd.Parameters.AddWithValue("@action", "IDHOADON");
            cmd.Parameters.AddWithValue("@paymentID", Convert.ToInt32(Request.QueryString["id"]));
            cmd.Parameters.AddWithValue("@userID", Session["userID"]);

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    grandTotal += Convert.ToDouble(row["tongtien"]);
                }
            }

            DataRow dr = dt.NewRow();
            dr["tongtien"] = grandTotal;
            dt.Rows.Add(dr);
            return dt;
        }
    }
}