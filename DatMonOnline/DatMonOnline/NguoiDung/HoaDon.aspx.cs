using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
//using System.Drawing;
using System.IO;
using System.Xml.Linq;


using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Net;


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
            if (!IsPostBack)
            {
                if (Session["userID"] != null)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        repeatThanhToan.DataSource = LayThongTinDatMon();
                        repeatThanhToan.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("DangNhap.aspx");
                }
            }
        }

        DataTable LayThongTinDatMon()
        {
            decimal grandTotal = 0;
            cn = new SqlConnection(KetNoi.LayChuoiKetNoi());
            cmd = new SqlCommand("HOADON", cn);
            cmd.Parameters.AddWithValue("@action", "IDHOADON");
            cmd.Parameters.AddWithValue("@paymentID", Convert.ToInt32(Request.QueryString["id"]));
            cmd.Parameters.AddWithValue("@userID", Session["userID"]);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    grandTotal += Convert.ToDecimal(row["tongtien"]);
                }
            }

            DataRow dr = dt.NewRow();
            dr["tongtien"] = grandTotal;
            dt.Rows.Add(dr);
            return dt;
        }

        protected void lbTaiXuongHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                string downloadPath = @"E:\hoa_don.pdf";
                DataTable dataTable = LayThongTinDatMon();
                XuatBaoCaoPDF(dataTable, downloadPath, "Hóa đơn");

                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(downloadPath);

                if(buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }
            }
            catch(Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Lỗi"+ ex.Message.ToString();
            }
        }

        void XuatBaoCaoPDF(DataTable dtblTable, String strPdfPath, string strHeader)
        {
            FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document();
            document.SetPageSize(PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            document.Open();

            //Report Header
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntHead = new Font(bfntHead, 16, 1, Color.GRAY);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
            document.Add(prgHeading);

            //Author
            Paragraph prgAuthor = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntAuthor = new Font(btnAuthor, 8, 2, Color.GRAY);
            prgAuthor.Alignment = Element.ALIGN_RIGHT;
            prgAuthor.Add(new Chunk("Đặt hàng từ : Foodie Fast Food", fntAuthor));
            prgAuthor.Add(new Chunk("\nNgày đặt : " + dtblTable.Rows[0]["ngaydat"].ToString(), fntAuthor));
            document.Add(prgAuthor);

            //Add a line seperation
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);

            //Add line break
            document.Add(new Chunk("\n", fntHead));

            //Write the table
            PdfPTable table = new PdfPTable(dtblTable.Columns.Count - 2);
            //Table header
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fntColumnHeader = new Font(btnColumnHeader, 9, 1, Color.WHITE);
            for (int i = 0; i < dtblTable.Columns.Count - 2; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = Color.GRAY;
                cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                table.AddCell(cell);
            }
            //table Data
            Font fntColumnData = new Font(btnColumnHeader, 8, 1, Color.BLACK);
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count - 2; j++)
                {
                    PdfPCell cell = new PdfPCell();
                    cell.AddElement(new Chunk(dtblTable.Rows[i][j].ToString(), fntColumnData));
                    table.AddCell(cell);
                }
            }

            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();

        }


    }
    }