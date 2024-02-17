using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatMonOnline.NguoiDung
{
    public partial class GioHang : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        decimal tongThanhTien = 0; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userID"] == null)
                {
                    Response.Redirect("DangNhap.aspx");
                }
                else
                {
                    LaySanPhamTuGioHang();
                }
            }
            
        }

        protected void repeatSanPhamGioHang_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Utils utils = new Utils();
            if (e.CommandName == "xoa")
            {
                cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
                cmd = new SqlCommand("GIOHANG_CRUD", cn);
                cmd.Parameters.AddWithValue("@action", "DELETE");
                cmd.Parameters.AddWithValue("@productID", e.CommandArgument);
                cmd.Parameters.AddWithValue("@userID", Session["userID"]);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    LaySanPhamTuGioHang();
                    // đếm số lượng sản phẩm trong giỏ
                    Session["demSoLuongGioHang"] = utils.demSoLuongGioHang(Convert.ToInt32(Session["userID"]));
                }
                catch (Exception ex)
                {
                    Response.Write("<script> alert('Lỗi - " + ex.Message + "'); </script>");
                }
                finally
                {
                    cn.Close();
                }
                
            }
            if(e.CommandName== "capnhat")
            {
                bool isCartUpdated = false;
                for(int i=0; i<repeatSanPhamGioHang.Items.Count; i++)
                {
                    if (repeatSanPhamGioHang.Items[i].ItemType == ListItemType.Item || repeatSanPhamGioHang.Items[i].ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox soluong = repeatSanPhamGioHang.Items[i].FindControl("txtSoLuong") as TextBox;
                        HiddenField _productID = repeatSanPhamGioHang.Items[i].FindControl("hdProductID") as HiddenField;
                        HiddenField _SoLuong = repeatSanPhamGioHang.Items[i].FindControl("hdSoLuong") as HiddenField;
                        int soluongGioHang = Convert.ToInt32(soluong.Text); // lấy từ textbox
                        int productID = Convert.ToInt32(_productID.Value);
                        int slTuDB = Convert.ToInt32(_SoLuong.Value);
                        bool isTrue = false;
                        int capNhatSoLuong = 1;
                        if (soluongGioHang > slTuDB)
                        {
                            capNhatSoLuong = soluongGioHang;
                            isTrue = true;
                        }
                        else if (soluongGioHang < slTuDB)
                        {
                            capNhatSoLuong = soluongGioHang;
                            isTrue = true;
                        }
                        if (isTrue)
                        {
                            // cập nhật số lượng sản phẩm vào database
                            isCartUpdated = utils.CapNhatSoLuongTrongGioHang(capNhatSoLuong, productID, Convert.ToInt32(Session["userID"]));
                        }
                    }
                }
                LaySanPhamTuGioHang();
            }
        }

        protected void repeatSanPhamGioHang_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label tongTien = e.Item.FindControl("lblTongTien") as Label;
                Label gia = e.Item.FindControl("lblGia") as Label;
                TextBox soLuong = e.Item.FindControl("txtSoLuong") as TextBox;

                decimal tinhTien = Convert.ToDecimal(gia.Text) * Convert.ToDecimal(soLuong.Text);
                tongTien.Text = tinhTien.ToString();
                tongThanhTien += tinhTien;
            }
            Session["tongThanhTien"] = tongThanhTien;
        }
        // lấy thông tin
        void LaySanPhamTuGioHang()
        {
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("GIOHANG_CRUD", cn);
            cmd.Parameters.AddWithValue("@action", "SELECT");            
            cmd.Parameters.AddWithValue("@userID", Session["userID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            repeatSanPhamGioHang.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                repeatSanPhamGioHang.FooterTemplate = null;
                repeatSanPhamGioHang.FooterTemplate = new TuyChinhMau(ListItemType.Footer);
            }
            repeatSanPhamGioHang.DataBind();
            
        }

        // 
        private sealed class TuyChinhMau : ITemplate
        {
            private ListItemType ListItemType { get; set; }
            public TuyChinhMau(ListItemType type)
            {
                ListItemType = type;
            }

            public void InstantiateIn(Control container)
            {
                if(ListItemType == ListItemType.Footer)
                {
                    var footer = new LiteralControl("<tr><td colspan='5'><b>Giỏ hàng trống.</b><a href='Menu.aspx' class='badge badge-info ml-2'>Tiếp tục mua sắm!</a></td></tr></tbody></table>");
                    container.Controls.Add(footer);
                }
            }
        }
    }
}