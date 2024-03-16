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
                ThanhToanDonHang(_ten, _soThe, _ngayHetHan, _CVV, _diaChi, _CheDoThanhToan);
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
                ThanhToanDonHang(_ten, _soThe, _ngayHetHan, _CVV, _diaChi, _CheDoThanhToan);
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
                // tương tác với bảng CHITIETDATHANG
                new DataColumn("sodonhang", typeof(string)),
                new DataColumn("productID", typeof(int)),
                new DataColumn("soluong", typeof(int)),
                new DataColumn("userID", typeof(int)),
                new DataColumn("trangthai", typeof(string)),
                new DataColumn("paymentID", typeof(int)),
                new DataColumn("ngaydathang", typeof(DateTime)) 
            });

            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cn.Open();

            #region sql transaction
            transaction = cn.BeginTransaction();
            cmd = new SqlCommand("LuuThongTinThanhToan", cn, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", ten);
            cmd.Parameters.AddWithValue("@sothe", sothe);
            cmd.Parameters.AddWithValue("@ngayHetHan", ngayHetHan);
            cmd.Parameters.AddWithValue("@cvv", cvv);
            cmd.Parameters.AddWithValue("@diachi", diaChi);
            cmd.Parameters.AddWithValue("@cheDoThanhToan", cheDoThanhToan);
            cmd.Parameters.AddWithValue("@insertedID", SqlDbType.Int);
            cmd.Parameters["@insertedID"].Direction = ParameterDirection.Output;
            try
            {
                cmd.ExecuteNonQuery();
                paymentID = Convert.ToInt32(cmd.Parameters["@insertedID"].Value);

                #region lấy sản phẩm
                cmd = new SqlCommand("GIOHANG_CRUD", cn, transaction);
                cmd.Parameters.AddWithValue("@action", "SELECT");
                cmd.Parameters.AddWithValue("@userID", Session["userID"]);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    productID = (int)dr["productID"];
                    quantity = (int)dr["soluong"];
                    // cập nhật số lượng món ăn
                    CapNhatSoLuong(productID, quantity, transaction, cn);
                    // xóa món ăn
                    XoaMonAn(productID, transaction, cn);

                    dt.Rows.Add(Utils.GetUniqueID(), productID, quantity, (int)Session["userID"], "Pending", paymentID, Convert.ToDateTime(DateTime.Now));
                }
                dr.Close();
                #endregion lấy sản phẩm

                #region Chi tiết thông tin đặt hàng
                if(dt.Rows.Count > 0)
                {
                    cmd = new SqlCommand("LuuThongTinDatHang", cn, transaction);
                    cmd.Parameters.AddWithValue("@tblOrders", dt);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                #endregion Chi tiết thông tin đặt hàng
                transaction.Commit();
                lblMessage.Visible = true;
                lblMessage.Text = "Đặt hàng thành công";
                lblMessage.CssClass = "alert alert-success";
                Response.AddHeader("REFRESH", "1;URL=HoaDon.aspx?id=" + paymentID);
            }
            catch (Exception e)
            {
                try
                {
                    transaction.Rollback();
                }
                catch(Exception ex)
                {
                    Response.Write("<script>alert('"+ex.Message+"');</script>");
                }
            }
            #endregion sql transaction
            finally
            {
                cn.Close();
            }
        }

        void CapNhatSoLuong(int _productID, int soluong, SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            int dbQuantity;
            cmd = new SqlCommand("SANPHAM_CRUD", sqlConnection, sqlTransaction);
            cmd.Parameters.AddWithValue("@action", "GETBYID");
            cmd.Parameters.AddWithValue("@productID", _productID);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                dr1 = cmd.ExecuteReader();
                while (dr1.Read())
                {
                    dbQuantity = (int)dr1["soluong"];
                    if(dbQuantity > soluong && dbQuantity > 2)
                    {
                        dbQuantity = dbQuantity - soluong;
                        cmd = new SqlCommand("SANPHAM_CRUD", sqlConnection, sqlTransaction);
                        cmd.Parameters.AddWithValue("@action", "CAPNHATSOLUONG");
                        cmd.Parameters.AddWithValue("@soluong", dbQuantity);
                        cmd.Parameters.AddWithValue("@productID", _productID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                dr1.Close();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }
        void XoaMonAn(int _productID, SqlTransaction sqlTransaction, SqlConnection sqlConnection)
        {
            cmd = new SqlCommand("GIOHANG_CRUD", sqlConnection, sqlTransaction);
            cmd.Parameters.AddWithValue("@action", "DELETE");
            cmd.Parameters.AddWithValue("@productID", _productID);
            cmd.Parameters.AddWithValue("@userID", Session["userID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        //protected void lbCODAddress_Click(object sender, EventArgs e){}

    }
}