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
    public partial class ThanhToan : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr, dr1;
        DataTable dt;
        SqlTransaction transaction = null;
        string _ten = string.Empty;
        string _soThe= string.Empty;
        string _ngayHetHan= string.Empty;
        string _CVV= string.Empty;
        string _diaChi= string.Empty;
        string _CheDoThanhToan= string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(Session["userID"] == null)
                {
                    Response.Redirect("DangNhap.aspx");
                }
            }
        }

        protected void lbCardSubmit_Click(object sender, EventArgs e)
        {
            _ten = txtName.Text.Trim();
            _soThe = txtSoThe.Text.Trim();
            _soThe = string.Format("************{0}", txtSoThe.Text.Trim().Substring(12, 4));
            _ngayHetHan = txtThangHetHan.Text.Trim() + "/" + txtNamHetHan.Text.Trim();
            _CVV = txtCVV.Text.Trim();
            _diaChi = txtDiaChi.Text.Trim();
            _CheDoThanhToan = "card";

            if (Session["userID"] != null)
            {

            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }


        protected void lbCODSubmit_Click(object sender, EventArgs e)
        {
            _diaChi = txtCODAddress.Text.Trim();
            _CheDoThanhToan = "cod";

            if (Session["userID"] != null)
            {

            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        void ThanhToanDonHang(string ten, string sothe, string ngayHetHan, string cvv, string diaChi, string cheDoThanhToan)
        {
            int paymentID; // mã thanh toán
            int productID; // mã món ăn
            int quantity;  // số lượng

            dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7]
            {
                // tương tác với bảng GOIMON
                new DataColumn("sodonhang", typeof(string)),
                new DataColumn("productID", typeof(int)),
                new DataColumn("soluong", typeof(int)),
                new DataColumn("userID", typeof(int)),
                new DataColumn("trangthai", typeof(string)),
                new DataColumn("paymentID", typeof(int)),
                new DataColumn("ngaytao", typeof(DateTime)) // ngayDatHang
            });

            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("LuuThongTinThanhToan", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ten", ten);
            cmd.Parameters.AddWithValue("@sothe", sothe);
            cmd.Parameters.AddWithValue("@ngayHetHan", ngayHetHan);
            cmd.Parameters.AddWithValue("@cvv", cvv);
            cmd.Parameters.AddWithValue("@diachi", diaChi);
            cmd.Parameters.AddWithValue("@cheDoThanhToan", cheDoThanhToan);
            cmd.Parameters.AddWithValue("@insertedID", SqlDbType.Int);
            cmd.Parameters["insertedID"].Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                paymentID = Convert.ToInt32(cmd.Parameters["insertedID"].Value);

                cmd = new SqlCommand("GIOHANG_CRUD", cn);
                cmd.Parameters.AddWithValue("@action", "SELECT");
                cmd.Parameters.AddWithValue("@userID", Session["userID"]);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    productID = (int)dr["productID"];
                    quantity = (int)dr["soluong"];
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                cn.Close();
            }
        }

        //protected void lbCODAddress_Click(object sender, EventArgs e){}

    }
}